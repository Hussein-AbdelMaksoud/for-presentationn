using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.ExistaceCase;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExistaceCaseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ExistaceCaseProfile _mapper;

        public ExistaceCaseController(IUnitOfWork unitOfWork, ExistaceCaseProfile mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all existace cases.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ExistanceCases = await _unitOfWork.ExistanceCases.GetAllAsync();
                if (ExistanceCases == null || !ExistanceCases.Any())
                {
                    return NotFound("No existace cases found.");
                }

                // Use Mapperly to convert the entity list to a DTO list.
                var dtos = _mapper.ToDtos(ExistanceCases);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                // In production, consider logging the exception details.
                return StatusCode(500, $"An error occurred while retrieving existace cases: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific existace case by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            try
            {
                var existaceCase = await _unitOfWork.ExistanceCases.GetByIdAsync(id);
                if (existaceCase == null)
                    return NotFound($"Existace case with ID {id} not found.");

                var dto = _mapper.ToDto(existaceCase);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the existace case: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific existace case by its Name.
        /// </summary>
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Invalid name.");

            try
            {
                // Retrieve the existace case by its name.
                var ExistanceCases = await _unitOfWork.ExistanceCases.FindAsync(a => a.Name.Contains(name));
                if (ExistanceCases == null || !ExistanceCases.Any())
                    return NotFound($"No existace case found with name '{name}'.");

                var output = _mapper.ToDtos(ExistanceCases);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the existace case: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new existace case.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExistaceCaseDTO existaceCase)
        {
            if (existaceCase == null)
                return BadRequest("Existace case data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Check if an existace case with the same name already exists.
                var existing = await _unitOfWork.ExistanceCases
                    .GetByNameAsync(a => a.Name.ToLower() == existaceCase.Name.ToLower());

                if (existing != null)
                {
                    return Conflict($"An existace case with the name '{existaceCase.Name}' already exists.");
                }

                // Map the DTO to the entity.
                var existaceCaseEntity = _mapper.ToEntity(existaceCase);
                await _unitOfWork.ExistanceCases.AddAsync(existaceCaseEntity);
                await _unitOfWork.SaveAsync();

                return Ok(existaceCase);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the existace case: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing existace case.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateExistaceCaseDTO existaceCaseDto)
        {
            if (existaceCaseDto == null)
                return BadRequest("Existace case data is required.");

            if (id <= 0)
                return BadRequest("Invalid id. id must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingExistaceCase = await _unitOfWork.ExistanceCases.GetByIdAsync(id);

                if (existingExistaceCase == null)
                    return NotFound($"Existace case with id {id} not found.");

                // Check if another existace case with the same name exists.
                var duplicateExistaceCase = await _unitOfWork.ExistanceCases
                    .FindAsync(s => s.Name.ToLower() == existaceCaseDto.Name.ToLower() && s.Id != id);

                if (duplicateExistaceCase.Any())
                    return Conflict($"An existace case with the name '{existaceCaseDto.Name}' already exists.");

                existingExistaceCase.Name = existaceCaseDto.Name;
                existingExistaceCase.Code = existaceCaseDto.Code;
                await _unitOfWork.SaveAsync();

                return Ok(existaceCaseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the existace case: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an existing existace case.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            try
            {
                var existaceCase = await _unitOfWork.ExistanceCases.GetByIdAsync(id);
                if (existaceCase == null)
                    return NotFound($"Existace case with ID {id} not found.");

                // Optional: Check for related dependencies before deletion.
                _unitOfWork.ExistanceCases.Delete(existaceCase.Id);
                await _unitOfWork.SaveAsync();

                return Ok("Existace case deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the existace case: {ex.Message}");
            }
        }
    }
}
