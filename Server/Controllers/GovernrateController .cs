using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.Governrate; // Ensure you have the corresponding DTO namespace
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernrateController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GovernrateProfile _mapper;

        public GovernrateController(IUnitOfWork unitOfWork, GovernrateProfile mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all governrates.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var governrates = await _unitOfWork.Governrates.GetAllAsync();
                if (governrates == null || !governrates.Any())
                {
                    return NotFound("No governrates found.");
                }

                // Map the entity list to a DTO list.
                var dtos = _mapper.ToDtos(governrates);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving governrates: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific governrate by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            try
            {
                var governrate = await _unitOfWork.Governrates.GetByIdAsync(id);
                if (governrate == null)
                    return NotFound($"Governrate with ID {id} not found.");

                var dto = _mapper.ToDto(governrate);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the governrate: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific governrate by its Name.
        /// </summary>
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Invalid name.");

            try
            {
                // Retrieve the governrate by its name.
                var governrates = await _unitOfWork.Governrates.FindAsync(a => a.Name.Contains(name));
                if (governrates == null || !governrates.Any())
                    return NotFound($"No governrate found with name '{name}'.");

                var output = _mapper.ToDtos(governrates);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the governrate: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new governrate.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGovernrateDTO governrateDto)
        {
            if (governrateDto == null)
                return BadRequest("Governrate data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Check if a governrate with the same name already exists.
                var existing = await _unitOfWork.Governrates
                    .GetByNameAsync(a => a.Name.ToLower() == governrateDto.Name.ToLower());

                if (existing != null)
                {
                    return Conflict($"A governrate with the name '{governrateDto.Name}' already exists.");
                }

                // Map the DTO to the entity.
                var governrateEntity = _mapper.ToEntity(governrateDto);
                await _unitOfWork.Governrates.AddAsync(governrateEntity);
                await _unitOfWork.SaveAsync();

                return Ok(governrateDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the governrate: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing governrate.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateGovernrateDTO governrateDto)
        {
            if (governrateDto == null)
                return BadRequest("Governrate data is required.");

            if (id <= 0)
                return BadRequest("Invalid id. id must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingGovernrate = await _unitOfWork.Governrates.GetByIdAsync(id);

                if (existingGovernrate == null)
                    return NotFound($"Governrate with id {id} not found.");

                // Check if another governrate with the same name exists.
                var duplicateGovernrate = await _unitOfWork.Governrates
                    .FindAsync(s => s.Name.ToLower() == governrateDto.Name.ToLower() && s.Id != id);

                if (duplicateGovernrate.Any())
                    return Conflict($"A governrate with the name '{governrateDto.Name}' already exists.");

                existingGovernrate.Name = governrateDto.Name;
                existingGovernrate.Code = governrateDto.Code;
                await _unitOfWork.SaveAsync();

                return Ok(governrateDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the governrate: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an existing governrate.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            try
            {
                var governrate = await _unitOfWork.Governrates.GetByIdAsync(id);
                if (governrate == null)
                    return NotFound($"Governrate with ID {id} not found.");

                // Optional: Check for related dependencies before deletion.
                _unitOfWork.Governrates.Delete(governrate.Id);
                await _unitOfWork.SaveAsync();

                return Ok("Governrate deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the governrate: {ex.Message}");
            }
        }
    }
}
