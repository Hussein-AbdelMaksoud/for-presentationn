using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Entities;
using Server.Models.DTOs;
using Server.Models.DTOs.GeneralAd;
using Server.Models.DTOs.SubAd;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;
using System.Collections.Generic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralAdController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GeneralAdProfile _mapper;

        public GeneralAdController(IUnitOfWork unitOfWork, GeneralAdProfile profile)
        {
            _unitOfWork = unitOfWork;
            _mapper = profile;
        }

        #region EndPoints

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<GeneralAd> generalAds = await _unitOfWork.GeneralAds.GetAllAsync();

                if (generalAds == null || !generalAds.Any())
                    return NotFound("No general ads found.");
                IEnumerable<GeneralAdDTO> output = new List<GeneralAdDTO>();
                output = _mapper.GeneralAdList_To_GeneralAdListDTO(generalAds);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retreving the General Ad: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var generalAd = await _unitOfWork.GeneralAds.GetByIdAsync(id);

                if (generalAd == null)
                    return NotFound($"General ad with ID {id} not found.");

                GeneralAdDTO output = new GeneralAdDTO();
                output = _mapper.GeneralAd_To_GeneralAdDTO(generalAd);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retreving the General Ad: {ex.Message}");
            }
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) )
                return BadRequest("please Enter a Valid Name.");

            try
            {
                var generalAds = await _unitOfWork.GeneralAds.FindAsync(a =>
                    a.Name.ToLower().Contains(name.ToLower()),new[] { "Sector" });

                IEnumerable<GeneralAdDTO> output = new List<GeneralAdDTO>();
                output = _mapper.GeneralAdList_To_GeneralAdListDTO(generalAds);
                
                if (!output.Any())
                    return NotFound($"No general ads found containing '{name}'.");

                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching for the General Ad: {ex.Message}");
            }
        }

        [HttpGet("BySector/{id}")]
        public async Task<IActionResult> GetGeneralAdBySector(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");
            try
            {
                Sector sector = await _unitOfWork.Sectors.GetByIdAsync(id);
                if (sector == null)
                    return NotFound($"Sector with ID {id} not found.");

                IEnumerable<GeneralAd> generalAds = await _unitOfWork.GeneralAds.FindAsync(a => a.SectorID == id);

                if (generalAds == null || !generalAds.Any())
                    return NotFound($"No general ads found for sector with ID {id}.");
                IEnumerable<GeneralAdDTO> output = new List<GeneralAdDTO>();
                output = _mapper.GeneralAdList_To_GeneralAdListDTO(generalAds);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retreving the General Ad: {ex.Message}");
            }
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGeneralAdDTO generalAdDto)
        {
            if (generalAdDto == null)
                return BadRequest("General ad data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (generalAdDto.SectorID <= 0)
                return BadRequest("Sector ID is must be a positive number.");

            if (_unitOfWork.SubAds.GetByIdAsync((int)generalAdDto.SectorID).Result == null)
                return BadRequest($"Sector with ID {generalAdDto.SectorID} not found.");

            try
            {
                GeneralAd generalAd = _mapper.CreateGeneralAdDTO_To_GeneralAd(generalAdDto);
                await _unitOfWork.GeneralAds.AddAsync(generalAd);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = generalAd.Id }, generalAd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the General Ad: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id,[FromBody] CreateGeneralAdDTO generalAdDto)
        {
            if (generalAdDto == null)
                return BadRequest("General ad data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (generalAdDto.SectorID <= 0)
                return BadRequest("Sector ID is must be a positive number.");

            if (_unitOfWork.SubAds.GetByIdAsync((int)generalAdDto.SectorID).Result == null)
                return BadRequest($"Sector with ID {generalAdDto.SectorID} not found.");

            try
            {
                var existingGeneralAd = await _unitOfWork.GeneralAds.GetByIdAsync(id);

                if (existingGeneralAd == null)
                    return NotFound($"General ad with ID {id} not found.");

                var duplicateGeneralAd = await _unitOfWork.GeneralAds
                    .FindAsync(ga => ga.Name.ToLower() == generalAdDto.Name.ToLower() && ga.Id != id);

                if (duplicateGeneralAd.Any())
                    return Conflict($"A general ad with the name '{generalAdDto.Name}' already exists.");

                existingGeneralAd.Code = generalAdDto.Code;
                existingGeneralAd.Name = generalAdDto.Name;
                existingGeneralAd.Status = generalAdDto.Status;
                existingGeneralAd.Level = generalAdDto.Level;
                existingGeneralAd.SpecialLevel = generalAdDto.SpecialLevel;
                existingGeneralAd.SectorID = generalAdDto.SectorID;

                await _unitOfWork.SaveAsync();

                return Ok(generalAdDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the General Ad: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var generalAd = await _unitOfWork.GeneralAds.GetByIdAsync(id);

                if (generalAd == null)
                    return NotFound($"General ad with ID {id} not found.");

                var relatedsubAds = await _unitOfWork.SubAds.FindAsync(sa => sa.GeneralAdId == id);
                if (relatedsubAds.Any())
                    return Conflict($"Cannot delete SubAd. {relatedsubAds.Count()} departments are associated with this SubAd.");

                _unitOfWork.GeneralAds.Delete(id);
                await _unitOfWork.SaveAsync();

                return Ok(generalAd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the General Ad: {ex.Message}");
            }
        }

        #endregion
    }
}
