using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Entities;
using Server.Mapping.Profile;
using Server.Services.UnitOfWork.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Server.Models.DTOs.FincialDegree;
using Server.Models.DTOs.GeneralAd;
using Server.Models.DTOs;

namespace Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FincialDegreesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FincialDegreeProfile _mapper;

        public FincialDegreesController(IUnitOfWork unitOfWork, FincialDegreeProfile mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        #region EndPoints

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var fincialDegrees = await _unitOfWork.FincialDegrees.GetAllAsync();
                if (!fincialDegrees.Any())
                    return NotFound("No Financial Degrees found.");

                // Use Mapperly to convert the entity list to a DTO list.
                var dtos = _mapper.MapFincialDegreeListToFincialDegreeDTOList(fincialDegrees);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Fincial Degrees: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id. Id must be a positive number.");

            try
            {
                var fincialDegree = await _unitOfWork.FincialDegrees.GetByIdAsync(id);
                if (fincialDegree == null)
                    return NotFound($"Financial Degree with Id {id} not found.");

                FincialDegreeDTO dto = _mapper.MapFincialDegreeToFincialDegreeDTO(fincialDegree);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the Fincial Degree: {ex.Message}");
            }
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                return BadRequest("Name must be at least 2 characters long.");

            try
            {
                var fincialDegrees = await _unitOfWork.FincialDegrees.FindAsync(a => a.Name.ToLower().Contains(name.ToLower()));

                if (!fincialDegrees.Any())
                    return NotFound($"No Financial Degrees found containing '{name}'.");

                FincialDegreeDTO output = _mapper.MapFincialDegreeToFincialDegreeDTO(fincialDegrees.First());
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching for Financial Degrees: {ex.Message}");
            }
        }

        //get by parent
        [HttpGet("ByFincialDegreeType/{id}")]
        public async Task<IActionResult> GetFincDegreeByFincDegreeType(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");
            try
            {
                FincialDegreeType fincialDegreeType = await _unitOfWork.FincialDegreeTypes.GetByIdAsync(id);
                if (fincialDegreeType == null)
                    return NotFound($"FincialDegreeType with ID {id} not found.");

                var fincialDegrees = await _unitOfWork.FincialDegrees.FindAsync(a => a.FincialDegreeTypeId == id);
                if (fincialDegrees == null || !fincialDegrees.Any())
                    return NotFound($"No FincialDegrees found for FincialDegreeType with ID {id}.");

                IEnumerable<FincialDegreeDTO> output = new List<FincialDegreeDTO>();

                output = _mapper.MapFincialDegreeListToFincialDegreeDTOList(fincialDegrees);
                return Ok(output);

            }
            catch
            {
                return StatusCode(500, $"An error occurred while retrieving FincialDegrees By FincialDegreeType with ID {id}.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFincialDegreeDTO fincialDegreeDto)
        {
            if (fincialDegreeDto == null)
                return BadRequest("Financial Degree data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Check if a Fincial degree with the same name already exists
                var existingFincialDegree = await _unitOfWork.FincialDegreeTypes
                    .FindAsync(f => f.Name.ToLower() == fincialDegreeDto.Name.ToLower());

                if (existingFincialDegree.Any())
                    return Conflict($"A Fincial degree with the name '{fincialDegreeDto.Name}' already exists.");

                if (fincialDegreeDto.FincialDegreeTypeId.HasValue)
                {
                    //check that finDegType can't be zero
                    if (fincialDegreeDto.FincialDegreeTypeId == 0)
                        return BadRequest("FincialDegreeTypeId cannot be zero.");

                    var fincialDegreeTypeExists = await _unitOfWork.FincialDegreeTypes.GetByIdAsync(fincialDegreeDto.FincialDegreeTypeId.Value);
                    if (fincialDegreeTypeExists == null)
                        return BadRequest($"No FincialDegreeType found with ID {fincialDegreeDto.FincialDegreeTypeId.Value}.FincialDegreeTypeId must match an existing record in the database");
                }

                // Map the DTO to the entity.
                FincialDegree fincialDegreeEntity = _mapper.MapCreateFincialDTOToFincialDegree(fincialDegreeDto);
                await _unitOfWork.FincialDegrees.AddAsync(fincialDegreeEntity);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = fincialDegreeEntity.Id }, fincialDegreeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Financial Degree: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateFincialDegreeDTO fincialDegreeDto)
        {
            if (fincialDegreeDto == null)
                return BadRequest("Financial Degree data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingFincialDegree = await _unitOfWork.FincialDegrees.GetByIdAsync(id);
                if (existingFincialDegree == null)
                    return NotFound($"Financial Degree with Id {id} not found.");

                // Update existing entity properties
                existingFincialDegree.Name = fincialDegreeDto.Name;
                existingFincialDegree.Code = fincialDegreeDto.Code;
              
            
                if (fincialDegreeDto.FincialDegreeTypeId.HasValue)
                {
                    //check that finDegType can't be zero
                    if (fincialDegreeDto.FincialDegreeTypeId == 0)
                        return BadRequest("FincialDegreeTypeId cannot be zero.");

                    var fincialDegreeTypeExists = await _unitOfWork.FincialDegreeTypes.GetByIdAsync(fincialDegreeDto.FincialDegreeTypeId.Value);
                    if (fincialDegreeTypeExists == null)
                        return BadRequest($"No FincialDegreeType found with ID {fincialDegreeDto.FincialDegreeTypeId.Value}.FincialDegreeTypeId must match an existing record in the database");
                }

                // Optional: Add any additional special level or other optional fields
                if (fincialDegreeDto.FincialDegreeTypeId.HasValue)
                    existingFincialDegree.FincialDegreeTypeId = fincialDegreeDto.FincialDegreeTypeId;

                await _unitOfWork.SaveAsync();

                return Ok(fincialDegreeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Financial Degree: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. Id must be a positive number.");

            try
            {
                var fincialDegree = await _unitOfWork.FincialDegrees.GetByIdAsync(id);
                if (fincialDegree == null)
                    return NotFound($"Financial Degree with Id {id} not found.");

                _unitOfWork.FincialDegrees.Delete(fincialDegree.Id);
                await _unitOfWork.SaveAsync();

                return Ok($"Fincial Degree with Id {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Financial Degree: {ex.Message}");
            }
        }

        #endregion
    }
}
