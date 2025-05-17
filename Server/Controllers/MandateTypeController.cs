using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.MandateType;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MandateTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MandateTypeProfile _profile;

        public MandateTypeController(IUnitOfWork unitOfWork, MandateTypeProfile profile)
        {
            _unitOfWork = unitOfWork;
            _profile = profile;
        }

        #region Endpoints

        /// <summary>
        /// GET: api/MandateType
        /// - **Description**: Retrieves all MandateType items from the database.
        /// - **URL**: `GET api/MandateType`
        /// - **Response**: A list of all MandateTypes.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var mandateTypes = await _unitOfWork.MandateTypes.GetAllAsync();
                if (!mandateTypes.Any())
                    return NotFound("No Mandate Types found.");

                var mandateTypeDtos = _profile.ToDTOs(mandateTypes);
                return Ok(mandateTypeDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Mandate Types: {ex.Message}");
            }
        }

        /// <summary>
        /// GET: api/MandateType/{id}
        /// - **Description**: Retrieves a specific MandateType by its ID.
        /// - **URL**: `GET api/MandateType/{id}`
        /// - **Response**: The MandateType item with the specified ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. Please provide a valid positive ID.");

            try
            {
                var mandateType = await _unitOfWork.MandateTypes.GetByIdAsync(id);
                if (mandateType == null)
                    return NotFound($"Mandate Type with ID {id} not found.");

                var mandateTypeDto = _profile.ToDTO(mandateType);
                return Ok(mandateTypeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the Mandate Type: {ex.Message}");
            }
        }

        /// <summary>
        /// GET: api/MandateType/by-name/{name}
        /// - **Description**: Searches for MandateType items containing the specified name.
        /// - **URL**: `GET api/MandateType/by-name/{name}`
        /// - **Response**: A list of MandateTypes that contain the given name.
        /// </summary>
        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name cannot be empty.");

            name = name.Trim();

            try
            {
                var mandateTypes = await _unitOfWork.MandateTypes.FindAsync(mt => mt.Name.ToLower().Contains(name.ToLower()));

                if (!mandateTypes.Any())
                    return NotFound($"No Mandate Type found with name '{name}'.");

                var mandateTypeDtos = _profile.ToDTOs(mandateTypes);
                return Ok(mandateTypeDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Mandate Type by name: {ex.Message}");
            }
        }

        /// <summary>
        /// POST: api/MandateType
        /// - **Description**: Creates a new MandateType item.
        /// - **URL**: `POST api/MandateType`
        /// - **Request Body**: JSON object containing MandateType data.
        /// - **Response**: The created MandateType item with its assigned ID.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMandateTypeDTO mandateTypeDto)
        {
            if (mandateTypeDto == null)
                return BadRequest("Mandate Type data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string mandateTypeName = mandateTypeDto.Name.Trim();

                var existingMandateType = await _unitOfWork.MandateTypes.FindAsync(mt => mt.Name.ToLower() == mandateTypeName.ToLower());

                if (existingMandateType.Any())
                    return Conflict($"A Mandate Type with Name {mandateTypeDto.Name} already exists.");

                var mandateType = _profile.ToCreateEntity(mandateTypeDto);

                await _unitOfWork.MandateTypes.AddAsync(mandateType);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = mandateType.Id }, _profile.ToDTO(mandateType));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Mandate Type: {ex.Message}");
            }
        }

        /// <summary>
        /// PUT: api/MandateType/{id}  
        /// - **Description**: Updates an existing MandateType item by ID.  
        /// - **URL**: `PUT api/MandateType/{id}`  
        /// - **Request Body**: JSON object containing updated MandateType data (Name, Code).  
        /// - **Response**: Returns the updated MandateType item.  
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateMandateTypeDTO mandateTypeDto)
        {
            if (mandateTypeDto == null)
                return BadRequest("Mandate Type data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingMandateType = await _unitOfWork.MandateTypes.GetByIdAsync(id);
                if (existingMandateType == null)
                    return NotFound($"Mandate Type with ID {id} not found.");

                string mandateTypeName = mandateTypeDto.Name.Trim();
                var duplicateMandateType = await _unitOfWork.MandateTypes.FindAsync(
                    mt => mt.Name.ToLower() == mandateTypeName.ToLower() && mt.Id != id
                );

                if (duplicateMandateType.Any())
                    return Conflict($"A Mandate Type with the name '{mandateTypeDto.Name}' already exists.");

                existingMandateType.Name = mandateTypeName;
                existingMandateType.Code = mandateTypeDto.Code;

                await _unitOfWork.SaveAsync();

                var updatedMandateTypeDTO = _profile.ToDTO(existingMandateType);
                return Ok(updatedMandateTypeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Mandate Type: {ex.Message}");
            }
        }

        /// <summary>
        /// DELETE: api/MandateType/{id}
        /// - **Description**: Deletes a MandateType item by its ID.
        /// - **URL**: `DELETE api/MandateType/{id}`
        /// - **Response**: Success message if the item is deleted.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var mandateType = await _unitOfWork.MandateTypes.GetByIdAsync(id);
                if (mandateType == null)
                    return NotFound($"Mandate Type with ID {id} not found.");

                _unitOfWork.MandateTypes.Delete(mandateType.Id);
                await _unitOfWork.SaveAsync();

                return Ok("Mandate Type deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Mandate Type: {ex.Message}");
            }
        }

        #endregion
    }
}
