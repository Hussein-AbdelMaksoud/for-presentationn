using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.NonExistanceType;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonExistanceTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly NonExistanceTypeProfile _profile;

        public NonExistanceTypeController(IUnitOfWork unitOfWork, NonExistanceTypeProfile profile)
        {
            _unitOfWork = unitOfWork;
            _profile = profile;
        }

        #region Endpoints

        /// <summary>
        /// GET: api/NonExistanceType
        /// - **Description**: Retrieves all NonExistanceType nonExistanceTypes from the database.
        /// - **URL**: `GET api/NonExistanceType`
        /// - **Response**: A list of all NonExistanceTypes.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var nonExistanceTypes = await _unitOfWork.NonExistanceTypes.GetAllAsync();
                if (!nonExistanceTypes.Any())
                    return NotFound("No Non Existance Types found.");

                var nonExistanceTypeDtos = _profile.ToDTOs(nonExistanceTypes);
                return Ok(nonExistanceTypeDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Non Existance Types: {ex.Message}");
            }
        }

        /// <summary>
        /// GET: api/NonExistanceType/{id}
        /// - **Description**: Retrieves a specific NonExistanceType by its ID.
        /// - **URL**: `GET api/NonExistanceType/{id}`
        /// - **Response**: The NonExistanceType nonExistanceType with the specified ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. Please provide a valid positive ID.");

            try
            {
                var nonExistanceType = await _unitOfWork.NonExistanceTypes.GetByIdAsync(id);
                if (nonExistanceType == null)
                    return NotFound($"Non Existance Type with ID {id} not found.");

                var nonExistanceTypeDto = _profile.ToDTO(nonExistanceType);
                return Ok(nonExistanceTypeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the Non Existance Type: {ex.Message}");
            }
        }

        /// <summary>
        /// GET: api/NonExistanceType/by-name/{name}
        /// - **Description**: Searches for NonExistanceType nonExistanceTypes containing the specified name.
        /// - **URL**: `GET api/NonExistanceType/by-name/{name}`
        /// - **Response**: A list of NonExistanceTypes that contain the given name.
        /// </summary>
        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name cannot be empty.");

            name = name.Trim();

            try
            {
                var nonExistanceTypes = await _unitOfWork.NonExistanceTypes.FindAsync(nt => nt.Name.ToLower().Contains(name.ToLower()));

                if (!nonExistanceTypes.Any())
                    return NotFound($"No Non Existance Type found with name '{name}'.");

                var nonExistanceTypeDtos = _profile.ToDTOs(nonExistanceTypes);
                return Ok(nonExistanceTypeDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Non Existance Type by name: {ex.Message}");
            }
        }

        /// <summary>
        /// POST: api/NonExistanceType
        /// - **Description**: Creates a new NonExistanceType nonExistanceType.
        /// - **URL**: `POST api/NonExistanceType`
        /// - **Request Body**: JSON object containing NonExistanceType data.
        /// - **Response**: The created NonExistanceType nonExistanceType with its assigned ID.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNonExistanceTypeDTO nonExistanceTypeDto)
        {
            if (nonExistanceTypeDto == null)
                return BadRequest("Non Existance Type data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string nonExistanceTypeName = nonExistanceTypeDto.Name.Trim();

                var existingnonExistanceType = await _unitOfWork.NonExistanceTypes.FindAsync(nt => nt.Name.ToLower() == nonExistanceTypeName.ToLower());

                if (existingnonExistanceType.Any())
                    return Conflict($"A Non Existance Type with Name {nonExistanceTypeDto.Name} already exists.");

                var nonExistanceType = _profile.ToCreateEntity(nonExistanceTypeDto);

                await _unitOfWork.NonExistanceTypes.AddAsync(nonExistanceType);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = nonExistanceType.Id }, _profile.ToDTO(nonExistanceType));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Non Existance Type: {ex.Message}");
            }
        }

        /// <summary>
        /// PUT: api/NonExistanceType/{id}  
        /// - **Description**: Updates an existing NonExistanceType nonExistanceType by ID.  
        /// - **URL**: `PUT api/NonExistanceType/{id}`  
        /// - **Request Body**: JSON object containing updated NonExistanceType data (Name, Code).  
        /// - **Response**: Returns the updated NonExistanceType nonExistanceType.  
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateNonExistanceTypeDTO nonExistanceTypeDto)
        {
            if (nonExistanceTypeDto == null)
                return BadRequest("Non Existance Type data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingnonExistanceType = await _unitOfWork.NonExistanceTypes.GetByIdAsync(id);
                if (existingnonExistanceType == null)
                    return NotFound($"Non Existance Type with ID {id} not found.");

                string nonExistanceTypeName = nonExistanceTypeDto.Name.Trim();
                var duplicatenonExistanceType = await _unitOfWork.NonExistanceTypes.FindAsync(
                    nt => nt.Name.ToLower() == nonExistanceTypeName.ToLower() && nt.Id != id
                );

                if (duplicatenonExistanceType.Any())
                    return Conflict($"A Non Existance Type with the name '{nonExistanceTypeDto.Name}' already exists.");

                existingnonExistanceType.Name = nonExistanceTypeName;
                existingnonExistanceType.Code = nonExistanceTypeDto.Code;

                await _unitOfWork.SaveAsync();

                var updatednonExistanceTypeDTO = _profile.ToDTO(existingnonExistanceType);
                return Ok(updatednonExistanceTypeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Non Existance Type: {ex.Message}");
            }
        }

        /// <summary>
        /// DELETE: api/NonExistanceType/{id}
        /// - **Description**: Deletes a NonExistanceType nonExistanceType by its ID.
        /// - **URL**: `DELETE api/NonExistanceType/{id}`
        /// - **Response**: Success message if the nonExistanceType is deleted.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var nonExistanceType = await _unitOfWork.NonExistanceTypes.GetByIdAsync(id);
                if (nonExistanceType == null)
                    return NotFound($"Non Existance Type with ID {id} not found.");

                _unitOfWork.NonExistanceTypes.Delete(nonExistanceType.Id);
                await _unitOfWork.SaveAsync();

                return Ok("Non Existance Type deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Non Existance Type: {ex.Message}");
            }
        }

        #endregion
    }
}
