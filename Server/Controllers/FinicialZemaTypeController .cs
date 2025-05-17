using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.FinicialZemaType;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinicialZemaTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FinicialZemaTypeProfile _mapper;

        public FinicialZemaTypeController(IUnitOfWork unitOfWork, FinicialZemaTypeProfile mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all finicial zema types.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var finicialZemaTypes = await _unitOfWork.FinicialZemaTypes.GetAllAsync();
                if (finicialZemaTypes == null || !finicialZemaTypes.Any())
                {
                    return NotFound("No finicial zema types found.");
                }

                // Use Mapperly to convert the entity list to a DTO list.
                var dtos = _mapper.ToDtos(finicialZemaTypes);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                // In production, consider logging the exception details.
                return StatusCode(500, $"An error occurred while retrieving finicial zema types: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific finicial zema type by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            try
            {
                var finicialZemaType = await _unitOfWork.FinicialZemaTypes.GetByIdAsync(id);
                if (finicialZemaType == null)
                    return NotFound($"Finicial zema type with ID {id} not found.");

                var dto = _mapper.ToDto(finicialZemaType);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the finicial zema type: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific finicial zema type by its Name.
        /// </summary>
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Invalid name.");

            try
            {
                // Retrieve the finicial zema type by its name.
                var finicialZemaTypes = await _unitOfWork.FinicialZemaTypes.FindAsync(a => a.Name.Contains(name));
                if (finicialZemaTypes == null || !finicialZemaTypes.Any())
                    return NotFound($"No finicial zema type found with name '{name}'.");

                var output = _mapper.ToDtos(finicialZemaTypes);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the finicial zema type: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new finicial zema type.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFinicialZemaTypeDTO finicialZemaType)
        {
            if (finicialZemaType == null)
                return BadRequest("Finicial zema type data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Check if a finicial zema type with the same name already exists.
                var existing = await _unitOfWork.FinicialZemaTypes
                    .GetByNameAsync(a => a.Name.ToLower() == finicialZemaType.Name.ToLower());

                if (existing != null)
                {
                    return Conflict($"A finicial zema type with the name '{finicialZemaType.Name}' already exists.");
                }

                // Map the DTO to the entity.
                var finicialZemaTypeEntity = _mapper.ToEntity(finicialZemaType);
                await _unitOfWork.FinicialZemaTypes.AddAsync(finicialZemaTypeEntity);
                await _unitOfWork.SaveAsync();

                return Ok(finicialZemaType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the finicial zema type: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing finicial zema type.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateFinicialZemaTypeDTO finicialZemaTypeDto)
        {
            if (finicialZemaTypeDto == null)
                return BadRequest("Finicial zema type data is required.");

            if (id <= 0)
                return BadRequest("Invalid id. id must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingFinicialZemaType = await _unitOfWork.FinicialZemaTypes.GetByIdAsync(id);

                if (existingFinicialZemaType == null)
                    return NotFound($"Finicial zema type with id {id} not found.");

                // Check if another finicial zema type with the same name exists.
                var duplicateFinicialZemaType = await _unitOfWork.FinicialZemaTypes
                    .FindAsync(s => s.Name.ToLower() == finicialZemaTypeDto.Name.ToLower() && s.Id != id);

                if (duplicateFinicialZemaType.Any())
                    return Conflict($"A finicial zema type with the name '{finicialZemaTypeDto.Name}' already exists.");

                existingFinicialZemaType.Name = finicialZemaTypeDto.Name;
                existingFinicialZemaType.Code = finicialZemaTypeDto.Code;
                await _unitOfWork.SaveAsync();

                return Ok(finicialZemaTypeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the finicial zema type: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an existing finicial zema type.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            try
            {
                var finicialZemaType = await _unitOfWork.FinicialZemaTypes.GetByIdAsync(id);
                if (finicialZemaType == null)
                    return NotFound($"Finicial zema type with ID {id} not found.");

                // Optional: Check for related dependencies before deletion.
                _unitOfWork.FinicialZemaTypes.Delete(finicialZemaType.Id);
                await _unitOfWork.SaveAsync();

                return Ok("Finicial zema type deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the finicial zema type: {ex.Message}");
            }
        }
    }
}
