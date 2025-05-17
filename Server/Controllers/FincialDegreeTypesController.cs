using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Entities;
using Server.Mapping.Profile;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;
using Server.Models.DTOs.FincialDegreeType;
using Server.Models.DTOs.BaseDTOs;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FincialDegreeTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FincialDegreeTypeProfile _mapper;
        public FincialDegreeTypeController(IUnitOfWork unitOfWork, FincialDegreeTypeProfile mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Endpoints

        /// Retrieves all fincialdegreetypes.
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var fincialDegreeTypes = await _unitOfWork.FincialDegreeTypes.GetAllAsync();

                if (fincialDegreeTypes == null || !fincialDegreeTypes.Any())
                {
                    return NotFound("No Fincial degree types found.");
                }
                // Use Mapperly to convert the entity list to a DTO list.
                var dtos = _mapper.MapFincialDegreeTypeListToBaseDTOList(fincialDegreeTypes);
                return Ok(dtos);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving Fincial degree types.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. Id must be a positive number.");

            try
            {
                var fincialDegreeType = await _unitOfWork.FincialDegreeTypes.GetByIdAsync(id);

                if (fincialDegreeType == null)
                    return NotFound($"Fincial degree type with Id {id} not found.");

                BaseDTO dto = _mapper.MapFincialDegreeTypeToBaseDTO(fincialDegreeType);
                return Ok(dto);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving the Fincial degree type.");
            }
        }
        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                return BadRequest("Name must be at least 2 characters long.");

            try
            {
                var fincialDegreeTypes = await _unitOfWork.FincialDegreeTypes.FindAsync(a => a.Name.ToLower().Contains(name.ToLower()));

                if (!fincialDegreeTypes.Any())
                    return NotFound($"No FincialDegreeType found containing '{name}'.");

                BaseDTO output = _mapper.MapFincialDegreeTypeToBaseDTO(fincialDegreeTypes.First());
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching for FincialDegreeTypes: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFincialDegreeTypeDTO fincialDegreeType)
        {
            if (fincialDegreeType == null)
                return BadRequest("Fincial degree type data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Check if a Fincial degree type with the same name already exists
                var existingFincialDegreeType = await _unitOfWork.FincialDegreeTypes
                    .FindAsync(f => f.Name.ToLower() == fincialDegreeType.Name.ToLower());

                if (existingFincialDegreeType.Any())
                    return Conflict($"A Fincial degree type with the name '{fincialDegreeType.Name}' already exists.");

                // Map the DTO to the entity.
                FincialDegreeType fincialDegreeTypeEntity = _mapper.MapCreateBaseDTOToFincialDegreeType(fincialDegreeType);

                await _unitOfWork.FincialDegreeTypes.AddAsync(fincialDegreeTypeEntity);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = fincialDegreeTypeEntity.Id }, fincialDegreeType);
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the Fincial degree type.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateBaseDTO fincialDegreeTypeDto)
        {
            if (fincialDegreeTypeDto == null)
                return BadRequest("Fincial degree type data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingFincialDegreeType = await _unitOfWork.FincialDegreeTypes.GetByIdAsync(id);

                if (existingFincialDegreeType == null)
                    return NotFound($"Fincial degree type with ID {id} not found.");

                // Check if another Fincial degree type with the same name exists
                var duplicateFincialDegreeType = await _unitOfWork.FincialDegreeTypes
                    .FindAsync(f => f.Name.ToLower() == fincialDegreeTypeDto.Name.ToLower() && f.Id != id);

                if (duplicateFincialDegreeType.Any())
                    return Conflict($"A Fincial degree type with the name '{fincialDegreeTypeDto.Name}' already exists.");

                existingFincialDegreeType.Code = fincialDegreeTypeDto.Code;
                existingFincialDegreeType.Name = fincialDegreeTypeDto.Name;
                await _unitOfWork.SaveAsync();

                return Ok(fincialDegreeTypeDto);
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the Fincial degree type.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id. Id must be a positive number.");

            try
            {
                var fincialDegreeType = await _unitOfWork.FincialDegreeTypes.GetByIdAsync(id);

                if (fincialDegreeType == null)
                    return NotFound($"Fincial degree type with Id {id} not found.");

                // Optional: Check for related entities before deletion
                var relatedFincialDegrees = await _unitOfWork.FincialDegrees.FindAsync(a => a.FincialDegreeTypeId == id);
                if (relatedFincialDegrees.Any())
                    return Conflict($"Cannot delete Fincial Degree Type. {relatedFincialDegrees.Count()} Fincial Degree are associated with this Fincial Degree Type.");


                _unitOfWork.FincialDegreeTypes.Delete(fincialDegreeType.Id);
                await _unitOfWork.SaveAsync();

                return Ok($"Fincial Degree Type With ID {id} deleted Successfully");
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the Fincial degree type.");
            }
        }
        #endregion
    }
}