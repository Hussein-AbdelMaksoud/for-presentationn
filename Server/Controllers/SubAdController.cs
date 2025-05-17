using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Entities;
using Server.Mapping.Profile;
using Server.Models.DTOs.SubAd;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubAdController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SubadProfile _mapper;

        public SubAdController(IUnitOfWork unitOfWork, SubadProfile subadProfile)
        {
            _unitOfWork = unitOfWork;
            _mapper = subadProfile;
        }

        #region EndPoints

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<SubAd> subAds = await _unitOfWork.SubAds.GetAllAsync();
                if (!subAds.Any())
                    return NotFound("No SubAds found.");
                IEnumerable<SubAdDTO> output = _mapper.SubAdList_To_SubAdDTOList(subAds);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving SubAds: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var subAd = await _unitOfWork.SubAds.GetByIdAsync(id);
                if (subAd == null)
                    return NotFound($"SubAd with ID {id} not found.");
                SubAdDTO output = _mapper.SubAd_To_SubAdDTO(subAd);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the SubAd: {ex.Message}");
            }
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                return BadRequest("Name must be at least 2 characters long.");

            try
            {
                var subAds = await _unitOfWork.SubAds.FindAsync(a => a.Name.ToLower().Contains(name.ToLower()));

                if (!subAds.Any())
                    return NotFound($"No SubAds found containing '{name}'.");

                IEnumerable<SubAdDTO> output = _mapper.SubAdList_To_SubAdDTOList(subAds);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching for SubAds: {ex.Message}");
            }
        }

        [HttpGet("ByGeneralAd/{id}")]
        public async Task<IActionResult> GetSubAdByGeneralAd(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");
            try
            {
                GeneralAd generalAd = await _unitOfWork.GeneralAds.GetByIdAsync(id);
                if (generalAd == null)
                    return NotFound($"generalAd with ID {id} not found.");

                var subAds = await _unitOfWork.SubAds.FindAsync(a => a.GeneralAdId == id);
                if (subAds == null || !subAds.Any())
                    return NotFound($"No subAds found for generalAd with ID {id}.");

                IEnumerable<SubAdDTO> output = _mapper.SubAdList_To_SubAdDTOList(subAds);
                return Ok(output);
            }
            catch
            {
                return StatusCode(500, $"An error occurred while retrieving subAds by generalAd {id}.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubAdDTO subAdDto)
        {
            if (subAdDto == null)
                return BadRequest("SubAd data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (subAdDto.GeneralAdId <= 0)
                return BadRequest("GeneralAd ID is must be a positive number.");

            if (_unitOfWork.SubAds.GetByIdAsync((int)subAdDto.GeneralAdId).Result == null)
                return BadRequest($"GeneralAd with ID {subAdDto.GeneralAdId} not found.");
            try
            {
                // Validate GeneralAd reference
                if (subAdDto.GeneralAdId.HasValue)
                {
                    var generalAdExists = await _unitOfWork.GeneralAds.GetByIdAsync(subAdDto.GeneralAdId.Value) != null;
                    if (!generalAdExists)
                        return BadRequest($"GeneralAd with ID {subAdDto.GeneralAdId} does not exist.");
                }

                SubAd subAd = _mapper.CreateSubAd_To_SubAd(subAdDto);
                await _unitOfWork.SubAds.AddAsync(subAd);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = subAd.Id }, subAdDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the SubAd: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int Id,[FromBody] CreateSubAdDTO subAdDto)
        {
            if (subAdDto == null)
                return BadRequest("SubAd data is required.");

            if (Id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (subAdDto.GeneralAdId <= 0)
                return BadRequest("GeneralAd ID is must be a positive number.");

            if (_unitOfWork.SubAds.GetByIdAsync((int)subAdDto.GeneralAdId).Result == null)
                return BadRequest($"GeneralAd with ID {subAdDto.GeneralAdId} not found.");
            try
            {
                var existingSubAd = await _unitOfWork.SubAds.GetByIdAsync(Id);
                if (existingSubAd == null)
                    return NotFound($"SubAd with ID {Id} not found.");

                // Validate GeneralAd reference
                if (subAdDto.GeneralAdId.HasValue)
                {
                    var generalAdExists = await _unitOfWork.GeneralAds.GetByIdAsync(subAdDto.GeneralAdId.Value) != null;
                    if (!generalAdExists)
                        return BadRequest($"GeneralAd with ID {subAdDto.GeneralAdId} does not exist.");
                }

                existingSubAd.Name = subAdDto.Name;
                existingSubAd.Level = subAdDto.Level;
                existingSubAd.SpecialLevel = subAdDto.SpecialLevel;
                existingSubAd.Status = subAdDto.Status;
                existingSubAd.GeneralAdId = subAdDto.GeneralAdId;

                await _unitOfWork.SaveAsync();

                return Ok(subAdDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the SubAd: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var subAd = await _unitOfWork.SubAds.GetByIdAsync(id);
                if (subAd == null)
                    return NotFound($"SubAd with ID {id} not found.");

                // Optional: Check for related Departments before deletion
                var relatedDepartments = await _unitOfWork.Departments.FindAsync(d => d.SubAdID == id);
                if (relatedDepartments.Any())
                    return Conflict($"Cannot delete SubAd. {relatedDepartments.Count()} departments are associated with this SubAd.");

                _unitOfWork.SubAds.Delete(id);
                await _unitOfWork.SaveAsync();

                return Ok(subAd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the SubAd: {ex.Message}");
            }
        }

        #endregion
    }
}
