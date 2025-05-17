using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Entities;
using Server.Mapping.Profile;
using Server.Models.DTOs;
using Server.Models.DTOs.Certificate;
using Server.Services.UnitOfWork.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CertificateProfile _mapper;

        public CertificateController(IUnitOfWork unitOfWork, CertificateProfile certificateProfile)
        {
            _unitOfWork = unitOfWork;
            _mapper = certificateProfile;
        }
        #region EndPoints

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Certificates = await _unitOfWork.Certificates.GetAllAsync();

                if (Certificates == null || !Certificates.Any())
                    return NotFound("No Certificates found.");

                IEnumerable<CertificateDTO> output = _mapper.CertificateList_To_CertificateDTOList(Certificates);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Certificates: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var Certificate = await _unitOfWork.Certificates.GetByIdAsync(id);

                if (Certificate == null)
                    return NotFound($"Certificate with ID {id} not found.");

                CertificateDTO output = _mapper.Certificate_To_CertificateDTO(Certificate);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the Certificate: {ex.Message}");
            }
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                return BadRequest("Name must be at least 2 characters long.");

            try
            {
                var lowerCaseName = name.ToLower();
                var Certificates = await _unitOfWork.Certificates.FindAsync(d => d.Name.ToLower().Contains(lowerCaseName));

                if (!Certificates.Any())
                    return NotFound($"No Certificates found containing '{name}'.");

                IEnumerable<CertificateDTO> output = _mapper.CertificateList_To_CertificateDTOList(Certificates);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching for Certificates: {ex.Message}");
            }
        }

        [HttpGet("BySubAd/{id}")]
        public async Task<IActionResult> GetCertificatesByEducationLevel(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");
            try
            {
                SubAd subAd = await _unitOfWork.SubAds.GetByIdAsync(id);
                if (subAd == null)
                    return NotFound($"Education level with ID {id} not found.");

                var Certificates = await _unitOfWork.Certificates.FindAsync(a => a.educationalLevelID == id);
                if (Certificates == null || !Certificates.Any())
                    return NotFound($"No Certificates found for Education Level with ID {id}.");

                IEnumerable<CertificateDTO> output = _mapper.CertificateList_To_CertificateDTOList(Certificates);
                return Ok(output);
            }
            catch
            {
                return StatusCode(500, $"An error occurred while retrieving Certificates by EducationLevel with ID {id}.");
            }
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCertificateDTO Certificate)
        {
            if (Certificate == null)
                return BadRequest("Certificate data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Certificate.EducationLevelId <= 0)
                return BadRequest("Education Level ID is must be a positive number.");

            if (_unitOfWork.SubAds.GetByIdAsync((int)Certificate.EducationLevelId).Result == null)
                return BadRequest($"Education Level with ID {Certificate.EducationLevelId} not found.");
            try
            {
                // Check if a Certificate with the same name already exists
                var existingCertificate = await _unitOfWork.Certificates.FindAsync(d => d.Name.ToLower() == Certificate.Name.ToLower());

                if (existingCertificate.Any())
                    return Conflict($"A Certificate with the name '{Certificate.Name}' already exists.");

                Certificate dept = _mapper.CreateCertificateDTO_To_Certificate(Certificate);
                await _unitOfWork.Certificates.AddAsync(dept);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = dept.Id }, Certificate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Certificate: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int Id, [FromBody] CreateCertificateDTO Certificate)
        {
            if (Certificate == null)
                return BadRequest("Certificate data is required.");

            if (Id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Certificate.EducationLevelId <= 0)
                return BadRequest("Education Level ID is must be a positive number.");

            if (_unitOfWork.SubAds.GetByIdAsync((int)Certificate.EducationLevelId).Result == null)
                return BadRequest($"Education Level with ID {Certificate.EducationLevelId} not found.");

            try
            {
                var existingCertificate = await _unitOfWork.Certificates.GetByIdAsync(Id);

                if (existingCertificate == null)
                    return NotFound($"Certificate with ID {Id} not found.");

                // Check if another Certificate with the same name exists
                var duplicateCertificate = await _unitOfWork.Certificates.FindAsync(d => d.Name.ToLower() == Certificate.Name.ToLower() && d.educationalLevelID != Certificate.EducationLevelId);

                if (duplicateCertificate.Any())
                    return Conflict($"A Certificate with the name '{Certificate.Name}' already exists.");

                existingCertificate.Name = Certificate.Name;
                existingCertificate.educationalLevelID = Certificate.EducationLevelId;
                await _unitOfWork.SaveAsync();

                return Ok(Certificate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Certificate: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var Certificate = await _unitOfWork.Certificates.GetByIdAsync(id);

                if (Certificate == null)
                    return NotFound($"Certificate with ID {id} not found.");

                _unitOfWork.Certificates.Delete(id);
                await _unitOfWork.SaveAsync();

                return Ok(Certificate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Certificate: {ex.Message}");
            }
        }

        #endregion
    }
}
