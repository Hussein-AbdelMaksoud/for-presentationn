using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.HealthState;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthStateController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HealthStateProfile _profile;

        public HealthStateController(IUnitOfWork unitOfWork, HealthStateProfile profile)
        {
            _unitOfWork = unitOfWork;
            _profile = profile;
        }

        #region Endpoints

        /// <summary>
        /// GET: api/HealthState
        /// - **Description**: Retrieves all HealthState items from the database.
        /// - **URL**: `GET api/HealthState`
        /// - **Response**: A list of all HealthStates.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var healthStates = await _unitOfWork.HealthStates.GetAllAsync();
                if (!healthStates.Any())
                    return NotFound("No Health States found.");

                var healthStatesDto = _profile.ToDTOs(healthStates);
                return Ok(healthStatesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Health States: {ex.Message}");
            }
        }

        /// <summary>
        /// GET: api/HealthState/{id}
        /// - **Description**: Retrieves a specific HealthState by its ID.
        /// - **URL**: `GET api/HealthState/{id}`
        /// - **Response**: The HealthState item with the specified ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. Please provide a valid positive ID.");

            try
            {
                var healthState = await _unitOfWork.HealthStates.GetByIdAsync(id);
                if (healthState == null)
                    return NotFound($"Health State with ID {id} not found.");

                var healthStateDto = _profile.ToDTO(healthState);
                return Ok(healthStateDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the Health State: {ex.Message}");
            }
        }

        /// <summary>
        /// GET: api/HealthState/by-name/{name}
        /// - **Description**: Searches for HealthState healthStates containing the specified name.
        /// - **URL**: `GET api/HealthState/by-name/{name}`
        /// - **Response**: A list of HealthStates that contain the given name.
        /// </summary>
        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name cannot be empty.");

            name = name.Trim();

            try
            {
                var healthStates = await _unitOfWork.HealthStates.FindAsync(nt => nt.Name.ToLower().Contains(name.ToLower()));

                if (!healthStates.Any())
                    return NotFound($"No Health State found with name '{name}'.");

                var healthStateDtos = _profile.ToDTOs(healthStates);
                return Ok(healthStateDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Health State by name: {ex.Message}");
            }
        }




        /// <summary>
        /// POST: api/HealthState
        /// - **Description**: Creates a new HealthState item.
        /// - **URL**: `POST api/HealthState`
        /// - **Request Body**: JSON object containing HealthState data.
        /// - **Response**: The created HealthState item with its assigned ID.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHealthStateDTO healthStateDto)
        {
            if (healthStateDto == null)
                return BadRequest("Health State data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string healthStateName = healthStateDto.Name.Trim();

                var existingHealthState = await _unitOfWork.HealthStates.FindAsync(
                    hs => hs.Name.ToLower() == healthStateName.ToLower());

                if (existingHealthState.Any())
                    return Conflict($"A Health State with Name {healthStateDto.Name} already exists.");

                var healthState = _profile.ToCreateEntity(healthStateDto);

                await _unitOfWork.HealthStates.AddAsync(healthState);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = healthState.Id }, _profile.ToDTO(healthState));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Health State: {ex.Message}");
            }
        }

        /// <summary>
        /// PUT: api/HealthState/{id}  
        /// - **Description**: Updates an existing HealthState item by ID.  
        /// - **URL**: `PUT api/HealthState/{id}`  
        /// - **Request Body**: JSON object containing updated HealthState data (Name, Code).  
        /// - **Response**: Returns the updated HealthState item.  
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateHealthStateDTO healthStateDto)
        {
            if (healthStateDto == null)
                return BadRequest("Health State data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingHealthState = await _unitOfWork.HealthStates.GetByIdAsync(id);
                if (existingHealthState == null)
                    return NotFound($"Health State with ID {id} not found.");

                string healthStateName = healthStateDto.Name.Trim();
                var duplicateHealthState = await _unitOfWork.HealthStates.FindAsync(
                    hs => hs.Name.ToLower() == healthStateName.ToLower() && hs.Id != id
                );

                if (duplicateHealthState.Any())
                    return Conflict($"A Health State with the name '{healthStateDto.Name}' already exists.");

                existingHealthState.Name = healthStateName;
                existingHealthState.Code = healthStateDto.Code;

                await _unitOfWork.SaveAsync();

                var updatedHealthStateDTO = _profile.ToDTO(existingHealthState);
                return Ok(updatedHealthStateDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Health State: {ex.Message}");
            }
        }

        /// <summary>
        /// DELETE: api/HealthState/{id}
        /// - **Description**: Deletes a HealthState item by its ID.
        /// - **URL**: `DELETE api/HealthState/{id}`
        /// - **Response**: Success message if the item is deleted.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var healthState = await _unitOfWork.HealthStates.GetByIdAsync(id);
                if (healthState == null)
                    return NotFound($"Health State with ID {id} not found.");

                _unitOfWork.HealthStates.Delete(healthState.Id);
                await _unitOfWork.SaveAsync();

                return Ok("HealthState deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Health State: {ex.Message}");
            }
        }

        #endregion
    }
}
