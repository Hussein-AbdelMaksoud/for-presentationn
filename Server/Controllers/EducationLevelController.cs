using Microsoft.AspNetCore.Mvc;
using Server.Data.Entities;
using Server.Mapping.Profile;
using Server.Models.DTOs.EducationLevel;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationalLevelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EducationLevelProfile _mapper;
        public EducationalLevelController(IUnitOfWork unitOfWork, EducationLevelProfile educationLevelProfile)
        {
            _unitOfWork = unitOfWork;
            _mapper = educationLevelProfile;
        }

        #region EndPoints

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var educationalLevels = await _unitOfWork.EducationalLevels.GetAllAsync();

                if (educationalLevels == null || !educationalLevels.Any())
                    return NotFound("No educational levels found.");

                IEnumerable<EducationLevelDTO> output = _mapper.Map(educationalLevels);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retreving the sector: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(short id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var educationalLevel = await _unitOfWork.EducationalLevels.GetByIdAsync(id);

                if (educationalLevel == null)
                    return NotFound($"Educational level with ID {id} not found.");

                EducationLevelDTO output = _mapper.Map(educationalLevel);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retreving the sector: {ex.Message}");
            }
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                return BadRequest("Name must be at least 2 characters long.");

            try
            {
                var educationalLevel = await _unitOfWork.EducationalLevels.FindAsync(a =>
                    a.Name.ToLower().Contains(name.ToLower()));

                if (!educationalLevel.Any())
                    return NotFound($"No educational levels found containing '{name}'.");

                IEnumerable<EducationLevelDTO> output = _mapper.Map(educationalLevel);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retreving the sector: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEducationLevelDTO educationalLevel)
        {
            if (educationalLevel == null)
                return BadRequest("Educational level data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Check if an educational level with the same name already exists
                var existingLevel = await _unitOfWork.EducationalLevels
                    .FindAsync(el => el.Name.ToLower() == educationalLevel.Name.ToLower());

                if (existingLevel.Any())
                    return Conflict($"An educational level with the name '{educationalLevel.Name}' already exists.");

                EducationalLevel level = _mapper.Map(educationalLevel);
                await _unitOfWork.EducationalLevels.AddAsync(level);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = level.Id }, educationalLevel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the sector: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int Id,[FromBody] CreateEducationLevelDTO educationalLevel)
        {
            if (educationalLevel == null)
                return BadRequest("Educational level data is required.");

            if (Id == 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingLevel = await _unitOfWork.EducationalLevels.GetByIdAsync(Id);

                if (existingLevel == null)
                    return NotFound($"Educational level with ID {Id} not found.");

                // Check if another educational level with the same name exists
                var duplicateLevel = await _unitOfWork.EducationalLevels
                    .FindAsync(el => el.Name.ToLower() == educationalLevel.Name.ToLower() && el.Id != Id);

                if (duplicateLevel.Any())
                    return Conflict($"An educational level with the name '{educationalLevel.Name}' already exists.");

                existingLevel.Name = educationalLevel.Name;
                existingLevel.SortId = educationalLevel.SortId;
                await _unitOfWork.SaveAsync();

                return Ok(educationalLevel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the sector: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var educationalLevel = await _unitOfWork.EducationalLevels.GetByIdAsync(id);

                if (educationalLevel == null)
                    return NotFound($"Educational level with ID {id} not found.");

                // Check if the educational level is used in any employee
                var relatedCertificates = await _unitOfWork.Certificates.FindAsync(e => e.educationalLevelID == id);

                if (relatedCertificates.Any())
                    return Conflict($"Cannot delete SubAd. {relatedCertificates.Count()} departments are associated with this SubAd.");

                _unitOfWork.EducationalLevels.Delete(id);
                await _unitOfWork.SaveAsync();

                return Ok(educationalLevel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the sector: {ex.Message}");
            }
        }
        #endregion
    }
}
