using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.Faculty; // Ensure you have the corresponding DTO namespace
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FacultyProfile _mapper;

        public FacultyController(IUnitOfWork unitOfWork, FacultyProfile mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all faculties.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var faculties = await _unitOfWork.Faculties.GetAllAsync();
                if (faculties == null || !faculties.Any())
                {
                    return NotFound("No faculties found.");
                }

                // Map the entity list to a DTO list.
                var dtos = _mapper.ToDtos(faculties);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                // In production, consider logging the exception details.
                return StatusCode(500, $"An error occurred while retrieving faculties: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific faculty by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            try
            {
                var faculty = await _unitOfWork.Faculties.GetByIdAsync(id);
                if (faculty == null)
                    return NotFound($"Faculty with ID {id} not found.");

                var dto = _mapper.ToDto(faculty);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the faculty: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific faculty by its Name.
        /// </summary>
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Invalid name.");

            try
            {
                // Retrieve the faculty by its name.
                var faculties = await _unitOfWork.Faculties.FindAsync(a => a.Name.Contains(name));
                if (faculties == null || !faculties.Any())
                    return NotFound($"No faculty found with name '{name}'.");

                var output = _mapper.ToDtos(faculties);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the faculty: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new faculty.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFacultyDTO facultyDto)
        {
            if (facultyDto == null)
                return BadRequest("Faculty data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Check if a faculty with the same name already exists.
                var existing = await _unitOfWork.Faculties
                    .GetByNameAsync(a => a.Name.ToLower() == facultyDto.Name.ToLower());

                if (existing != null)
                {
                    return Conflict($"A faculty with the name '{facultyDto.Name}' already exists.");
                }

                // Map the DTO to the entity.
                var facultyEntity = _mapper.ToEntity(facultyDto);
                await _unitOfWork.Faculties.AddAsync(facultyEntity);
                await _unitOfWork.SaveAsync();

                return Ok(facultyDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the faculty: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing faculty.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateFacultyDTO facultyDto)
        {
            if (facultyDto == null)
                return BadRequest("Faculty data is required.");

            if (id <= 0)
                return BadRequest("Invalid id. id must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingFaculty = await _unitOfWork.Faculties.GetByIdAsync(id);

                if (existingFaculty == null)
                    return NotFound($"Faculty with id {id} not found.");

                // Check if another faculty with the same name exists.
                var duplicateFaculty = await _unitOfWork.Faculties
                    .FindAsync(s => s.Name.ToLower() == facultyDto.Name.ToLower() && s.Id != id);

                if (duplicateFaculty.Any())
                    return Conflict($"A faculty with the name '{facultyDto.Name}' already exists.");

                existingFaculty.Name = facultyDto.Name;
                existingFaculty.Code = facultyDto.Code;
                await _unitOfWork.SaveAsync();

                return Ok(facultyDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the faculty: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an existing faculty.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            try
            {
                var faculty = await _unitOfWork.Faculties.GetByIdAsync(id);
                if (faculty == null)
                    return NotFound($"Faculty with ID {id} not found.");

                // Optional: Check for related dependencies before deletion.
                _unitOfWork.Faculties.Delete(faculty.Id);
                await _unitOfWork.SaveAsync();

                return Ok("Faculty deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the faculty: {ex.Message}");
            }
        }
    }
}
