using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.MilitaryStateType;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilitaryStateTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MilitaryStateTypeProfile _profile;

        public MilitaryStateTypeController(IUnitOfWork unitOfWork, MilitaryStateTypeProfile profile)
        {
            _unitOfWork = unitOfWork;
            _profile = profile;
        }

        #region Endpoints

        /// <summary>
        /// GET: api/MilitaryStateType
        /// - **Description**: Retrieves all MilitaryStateType items from the database.
        /// - **URL**: `GET api/MilitaryStateType`
        /// - **Response**: A list of all MilitaryStateTypes.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var militaryStateTypes = await _unitOfWork.MilitaryStateTypes.GetAllAsync();
                if (!militaryStateTypes.Any())
                    return NotFound("No Military State Types found.");

                var militaryStateTypeDtos = _profile.ToDTOs(militaryStateTypes);
                return Ok(militaryStateTypeDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Military State Types: {ex.Message}");
            }
        }

        /// <summary>
        /// GET: api/MilitaryStateType/{id}
        /// - **Description**: Retrieves a specific MilitaryStateType by its ID.
        /// - **URL**: `GET api/MilitaryStateType/{id}`
        /// - **Response**: The MilitaryStateType item with the specified ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. Please provide a valid positive ID.");

            try
            {
                var militaryStateType = await _unitOfWork.MilitaryStateTypes.GetByIdAsync(id);
                if (militaryStateType == null)
                    return NotFound($"Military State Type with ID {id} not found.");

                var militaryStateTypeDto = _profile.ToDTO(militaryStateType);
                return Ok(militaryStateTypeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the Military State Type: {ex.Message}");
            }
        }

        /// <summary>
        /// GET: api/MilitaryStateType/by-name/{name}
        /// - **Description**: Searches for MilitaryStateType items containing the specified name.
        /// - **URL**: `GET api/MilitaryStateType/by-name/{name}`
        /// - **Response**: A list of MilitaryStateTypes that contain the given name.
        /// </summary>
        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name cannot be empty.");

            name = name.Trim();

            try
            {
                var militaryStateTypes = await _unitOfWork.MilitaryStateTypes.FindAsync(mst => mst.Name.ToLower().Contains(name.ToLower()));

                if (!militaryStateTypes.Any())
                    return NotFound($"No Military State Type found with name '{name}'.");

                var militaryStateTypeDtos = _profile.ToDTOs(militaryStateTypes);
                return Ok(militaryStateTypeDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Military State Type by name: {ex.Message}");
            }
        }

        /// <summary>
        /// POST: api/MilitaryStateType
        /// - **Description**: Creates a new MilitaryStateType item.
        /// - **URL**: `POST api/MilitaryStateType`
        /// - **Request Body**: JSON object containing MilitaryStateType data.
        /// - **Response**: The created MilitaryStateType item with its assigned ID.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMilitaryStateTypeDTO militaryStateTypeDto)
        {
            if (militaryStateTypeDto == null)
                return BadRequest("Military State Type data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string militaryStateTypeName = militaryStateTypeDto.Name.Trim();

                var existingMilitaryStateType = await _unitOfWork.MilitaryStateTypes.FindAsync(mst => mst.Name.ToLower() == militaryStateTypeName.ToLower());

                if (existingMilitaryStateType.Any())
                    return Conflict($"A Military State Type with Name {militaryStateTypeDto.Name} already exists.");

                var militaryStateType = _profile.ToCreateEntity(militaryStateTypeDto);

                await _unitOfWork.MilitaryStateTypes.AddAsync(militaryStateType);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = militaryStateType.Id }, _profile.ToDTO(militaryStateType));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Military State Type: {ex.Message}");
            }
        }

        /// <summary>
        /// PUT: api/MilitaryStateType/{id}  
        /// - **Description**: Updates an existing MilitaryStateType item by ID.  
        /// - **URL**: `PUT api/MilitaryStateType/{id}`  
        /// - **Request Body**: JSON object containing updated MilitaryStateType data (Name, Code).  
        /// - **Response**: Returns the updated MilitaryStateType item.  
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateMilitaryStateTypeDTO militaryStateTypeDto)
        {
            if (militaryStateTypeDto == null)
                return BadRequest("Military State Type data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingMilitaryStateType = await _unitOfWork.MilitaryStateTypes.GetByIdAsync(id);
                if (existingMilitaryStateType == null)
                    return NotFound($"Military State Type with ID {id} not found.");

                string militaryStateTypeName = militaryStateTypeDto.Name.Trim();
                var duplicateMilitaryStateType = await _unitOfWork.MilitaryStateTypes.FindAsync(
                    mst => mst.Name.ToLower() == militaryStateTypeName.ToLower() && mst.Id != id
                );

                if (duplicateMilitaryStateType.Any())
                    return Conflict($"A Military State Type with the name '{militaryStateTypeDto.Name}' already exists.");

                existingMilitaryStateType.Name = militaryStateTypeName;
                existingMilitaryStateType.Code = militaryStateTypeDto.Code;

                await _unitOfWork.SaveAsync();

                var updatedMilitaryStateTypeDTO = _profile.ToDTO(existingMilitaryStateType);
                return Ok(updatedMilitaryStateTypeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Military State Type: {ex.Message}");
            }
        }

        /// <summary>
        /// DELETE: api/MilitaryStateType/{id}
        /// - **Description**: Deletes a MilitaryStateType item by its ID.
        /// - **URL**: `DELETE api/MilitaryStateType/{id}`
        /// - **Response**: Success message if the item is deleted.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var militaryStateType = await _unitOfWork.MilitaryStateTypes.GetByIdAsync(id);
                if (militaryStateType == null)
                    return NotFound($"Military State Type with ID {id} not found.");

                _unitOfWork.MilitaryStateTypes.Delete(militaryStateType.Id);
                await _unitOfWork.SaveAsync();

                return Ok("Military State deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Military State Type: {ex.Message}");
            }
        }

        #endregion
    }
}
