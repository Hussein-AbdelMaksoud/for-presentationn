using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using Server.Data.Entities;
using Server.Models.DTOs.Sector;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SectorProfile _mapper;


        public SectorController(IUnitOfWork unitOfWork, SectorProfile sectorprofile)
        {
            _unitOfWork = unitOfWork;
            _mapper = sectorprofile;
        }

        #region EndPoints
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<Sector> sectors = await _unitOfWork.Sectors.GetAllAsync();

                if (sectors == null || !sectors.Any())
                    return NotFound("No sectors found.");
                var sectorsDTO = _mapper.SectorList_To_SectorDTOList(sectors);
                return Ok(sectorsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retreving the sector: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var sector = await _unitOfWork.Sectors.GetByIdAsync(id);

                if (sector == null)
                    return NotFound($"Sector with ID {id} not found.");
                SectorDTO sectorDTO = _mapper.Sector_To_SectorDTO(sector);
                return Ok(sectorDTO);
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
                var sectors = await _unitOfWork.Sectors.FindAsync(a =>
                    a.Name.ToLower().Contains(name.ToLower()));

                if (!sectors.Any())
                    return NotFound($"No sectors found containing '{name}'.");

                SectorDTO sectorDTO = _mapper.Sector_To_SectorDTO(sectors.First());
                return Ok(sectorDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching for sector: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSectorDTO sectorDTO)
        {
            if (sectorDTO == null)
                return BadRequest("Sector data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Check if a sector with the same name already exists
                var existingSector = await _unitOfWork.Sectors
                    .FindAsync(s => s.Name.ToLower() == sectorDTO.Name.ToLower());

                if (existingSector.Any())
                    return Conflict($"A sector with the name '{sectorDTO.Name}' already exists.");

                Sector sector = _mapper.CreateSectorDTO_To_Sector(sectorDTO);

                await _unitOfWork.Sectors.AddAsync(sector);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = sector.Id }, sectorDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the sector: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] CreateSectorDTO sectorDTO)
        {
            if (sectorDTO == null)
                return BadRequest("Sector data is required.");

            if (id <= 0)
                return BadRequest("Invalid id. id must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingSector = await _unitOfWork.Sectors.GetByIdAsync(id);

                if (existingSector == null)
                    return NotFound($"Sector with id {id} not found.");

                // Check if another sector with the same name exists
                var duplicateSector = await _unitOfWork.Sectors
                    .FindAsync(s => s.Name.ToLower() == sectorDTO.Name.ToLower() && s.Id != id);

                if (duplicateSector.Any())
                    return Conflict($"A sector with the name '{sectorDTO.Name}' already exists.");
            
                existingSector.Name = sectorDTO.Name;
                existingSector.Code = sectorDTO.Code;
                existingSector.Status = sectorDTO.Status;
                await _unitOfWork.SaveAsync();

                return Ok(sectorDTO);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the sector: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var sector = await _unitOfWork.Sectors.GetByIdAsync(id);

                if (sector == null)
                    return NotFound($"Sector with ID {id} not found.");

                // Optional: Check for related entities before deletion
                var relatedGeneralAds = await _unitOfWork.GeneralAds.FindAsync(ga => ga.SectorID == id);

                if (relatedGeneralAds.Any())
                    return Conflict($"Cannot delete sector. {relatedGeneralAds.Count()} general ads are associated with this sector.");

                _unitOfWork.Sectors.Delete(id);
                await _unitOfWork.SaveAsync();

                return Ok(sector);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the sector: {ex.Message}");
            }
        }
        #endregion

    }
}
