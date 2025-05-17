using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.AdKind;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdKindController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AdKindProfile _profile;

        public AdKindController(IUnitOfWork unitOfWork, AdKindProfile profile)
        {
            _unitOfWork = unitOfWork;
            _profile = profile;
        }

        #region Endpoints

        /// <summary>
        /// GET: api/AdKind
        /// - **Description**: Retrieves all AdKind items from the database.
        /// - **URL**: `GET api/AdKind`
        /// - **Response**: A list of all AdKinds.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var adKinds = await _unitOfWork.AdKinds.GetAllAsync();
                if (!adKinds.Any())
                    return NotFound("No Ad Kinds found.");

                var adKindsDto = _profile.ToDTOs(adKinds);
                return Ok(adKindsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Ad Kinds: {ex.Message}");
            }
        }

        /// <summary>
        /// GET: api/AdKind/{id}
        /// - **Description**: Retrieves a specific AdKind by its ID.
        /// - **URL**: `GET api/AdKind/{id}`
        /// - **Response**: The AdKind item with the specified ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. Please provide a valid positive ID.");

            try
            {
                var adKind = await _unitOfWork.AdKinds.GetByIdAsync(id);
                if (adKind == null)
                    return NotFound($"Ad Kind with ID {id} not found.");

                var adKindDto = _profile.ToDTO(adKind);
                return Ok(adKindDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the Ad Kind: {ex.Message}");
            }
        }

        /// <summary>
        /// GET: api/AdKind/by-name/{name}
        /// - **Description**: Searches for AdKind items containing the specified name.
        /// - **URL**: `GET api/AdKind/by-name/{name}`
        /// - **Response**: A list of AdKinds that contain the given name.
        /// </summary>
        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name cannot be empty.");

            name = name.Trim();

            try
            {
                var adKinds = await _unitOfWork.AdKinds.FindAsync(ak => ak.Name.ToLower().Contains(name.ToLower()));

                if (!adKinds.Any())
                    return NotFound($"No Ad Kind found with name '{name}'.");

                var output = _profile.ToDTOs(adKinds);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Ad Kind by name: {ex.Message}");
            }
        }

        /// <summary>
        /// POST: api/AdKind
        /// - **Description**: Creates a new AdKind item.
        /// - **URL**: `POST api/AdKind`
        /// - **Request Body**: JSON object containing AdKind data.
        /// - **Response**: The created AdKind item with its assigned ID.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdKindDTO adKindDto)
        {
            if (adKindDto == null)
                return BadRequest("Ad Kind data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string adKindName = adKindDto.Name.Trim();

                var existingAdKind = await _unitOfWork.AdKinds.FindAsync(ak => ak.Name.ToLower() == adKindName.ToLower());

                if (existingAdKind.Any())
                    return Conflict($"An Ad Kind with Name {adKindDto.Name} already exists.");

                var adKind = _profile.ToCreateEntity(adKindDto);

                await _unitOfWork.AdKinds.AddAsync(adKind);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = adKind.Id }, _profile.ToDTO(adKind));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Ad Kind: {ex.Message}");
            }
        }

        /// <summary>
        /// PUT: api/AdKind/{id}  
        /// - **Description**: Updates an existing AdKind item by ID.  
        /// - **URL**: `PUT api/AdKind/{id}`  
        /// - **Request Body**: JSON object containing updated AdKind data (Name, Code).  
        /// - **Response**: Returns the updated AdKind item.  
        /// </summary>

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateAdKindDTO adKindDto)
        {
            if (adKindDto == null)
                return BadRequest("Ad Kind data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingAdKind = await _unitOfWork.AdKinds.GetByIdAsync(id);
                if (existingAdKind == null)
                    return NotFound($"Ad Kind with ID {id} not found.");

                string adKindName = adKindDto.Name.Trim();
                var duplicateAdKind = await _unitOfWork.AdKinds.FindAsync(
                    ak => ak.Name.ToLower() == adKindName.ToLower() && ak.Id != id
                );

                if (duplicateAdKind.Any())
                    return Conflict($"An Ad Kind with the name '{adKindDto.Name}' already exists.");

                existingAdKind.Name = adKindName;
                existingAdKind.Code = adKindDto.Code;

               
                await _unitOfWork.SaveAsync();

                var updatedAdKindDTO = _profile.ToDTO(existingAdKind);
                return Ok(updatedAdKindDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Ad Kind: {ex.Message}");
            }
        }

        /// <summary>
        /// DELETE: api/AdKind/{id}
        /// - **Description**: Deletes an AdKind item by its ID.
        /// - **URL**: `DELETE api/AdKind/{id}`
        /// - **Response**: Success message if the item is deleted.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var adKind = await _unitOfWork.AdKinds.GetByIdAsync(id);
                if (adKind == null)
                    return NotFound($"Ad Kind with ID {id} not found.");

                _unitOfWork.AdKinds.Delete(adKind.Id);
                await _unitOfWork.SaveAsync();

                return Ok("Ad Kind deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Ad Kind: {ex.Message}");
            }
        }

        #endregion
    }
}
