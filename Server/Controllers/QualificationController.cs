using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Entities;
using Server.Mapping.Profile;
using Server.Models.DTOs.Qualification;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualificationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly QualificationProfile _mapper;

        public QualificationController(IUnitOfWork unitOfWork, QualificationProfile Qualificationprofile)
        {
            _unitOfWork = unitOfWork;
            _mapper = Qualificationprofile;
        }

        #region EndPoints

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Qualifications = await _unitOfWork.Qualifications.FindAsync(a => a.Id>0, ["Employee"]);

                if (Qualifications == null || !Qualifications.Any())
                    return NotFound("No Qualifications found.");

                IEnumerable<QualificationDTO> output = _mapper.QualificationList_To_QualificationDTOList(Qualifications);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Qualifications: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var Qualification = await _unitOfWork.Qualifications.FindAsync(a => a.Id == id, ["Employee"]);

                if (Qualification == null)
                    return NotFound($"Qualification with ID {id} not found.");

                IEnumerable<QualificationDTO> output = _mapper.QualificationList_To_QualificationDTOList(Qualification);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the Qualification: {ex.Message}");
            }
        }

        [HttpGet("ByName/{Specialization}")]
        public async Task<IActionResult> GetBySpecialization(string Specialization)
        {
            if (string.IsNullOrWhiteSpace(Specialization) || Specialization.Length < 2)
                return BadRequest("Specialization must be at least 2 characters long.");

            try
            {
                var lowerCaseName = Specialization.ToLower();
                var Qualifications = await _unitOfWork.Qualifications.FindAsync(d => d.Specialization.ToLower().Contains(lowerCaseName), ["Employee"]);

                if (!Qualifications.Any())
                    return NotFound($"No Qualifications found containing '{Specialization}'.");

                IEnumerable<QualificationDTO> output = _mapper.QualificationList_To_QualificationDTOList(Qualifications);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching for Qualifications: {ex.Message}");
            }
        }

        [HttpGet("ByCertificate/{id}")]
        public async Task<IActionResult> GetByCertificate(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");
            try
            {
                Certificate certificate = await _unitOfWork.Certificates.GetByIdAsync(id);
                if (certificate == null)
                    return NotFound($"Certificate with ID {id} not found.");

                var Qualifications = await _unitOfWork.Qualifications.FindAsync(a => a.CertificateID == id, ["Employee"]);
                if (Qualifications == null || !Qualifications.Any())
                    return NotFound($"No Qualifications found for Certificate with ID {id}.");

                IEnumerable<QualificationDTO> output = _mapper.QualificationList_To_QualificationDTOList(Qualifications);
                return Ok(output);
            }
            catch
            {
                return StatusCode(500, $"An error occurred while retrieving Qualifications by Certificate with ID {id}.");
            }
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateQualificationDTO Qualification)
        {
            if (Qualification == null)
                return BadRequest("Qualification data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Qualification.CertificateID<= 0)
                return BadRequest("Certificate ID is must be a positive number.");

            if (_unitOfWork.SubAds.GetByIdAsync((int)Qualification.CertificateID).Result == null)
                return BadRequest($"Certificate with ID {Qualification.CertificateID} not found.");

            if (Qualification.QualGradeID <= 0)
                return BadRequest("Qualification Grade ID is must be a positive number.");

            if (_unitOfWork.QualGrades.GetByIdAsync((int)Qualification.QualGradeID).Result == null)
                return BadRequest($"Qualification Grade with ID {Qualification.QualGradeID} not found.");

            try
            {

                Qualification Qual = _mapper.CreateQualification_To_Qualification(Qualification);
                await _unitOfWork.Qualifications.AddAsync(Qual);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = Qual.Id }, Qualification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Qualification: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int Id, [FromBody] CreateQualificationDTO Qualification)
        {
            if (Qualification == null)
                return BadRequest("Qualification data is required.");

            if (Id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Qualification.CertificateID <= 0)
                return BadRequest("Certificate ID is must be a positive number.");

            if (_unitOfWork.SubAds.GetByIdAsync((int)Qualification.CertificateID).Result == null)
                return BadRequest($"Certificate with ID {Qualification.CertificateID} not found.");

            if (Qualification.QualGradeID <= 0)
                return BadRequest("Qualification Grade ID is must be a positive number.");

            if (_unitOfWork.QualGrades.GetByIdAsync((int)Qualification.QualGradeID).Result == null)
                return BadRequest($"Qualification Grade with ID {Qualification.QualGradeID} not found.");
            try
            {
                var existingQualification = await _unitOfWork.Qualifications.GetByIdAsync(Id);

                if (existingQualification == null)
                    return NotFound($"Qualification with ID {Id} not found.");

                existingQualification.Specialization = Qualification.Specialization;
                existingQualification.Code = Qualification.Code;
                existingQualification.CertificateID = Qualification.CertificateID;
                existingQualification.QualGradeID = Qualification.QualGradeID;
                existingQualification.DecisionNumber = Qualification.DecisionNumber;
                existingQualification.DecisionDate = Qualification.DecisionDate;
                existingQualification.GraduationDate = Qualification.GraduationDate;
                existingQualification.LastQual = Qualification.LastQual;
                existingQualification.QualPlace = Qualification.QualPlace;
                existingQualification.NationalID = Qualification.NationalID;

                await _unitOfWork.SaveAsync();

                return Ok(Qualification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Qualification: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var Qualification = await _unitOfWork.Qualifications.GetByIdAsync(id);

                if (Qualification == null)
                    return NotFound($"Qualification with ID {id} not found.");

                _unitOfWork.Qualifications.Delete(id);
                await _unitOfWork.SaveAsync();

                return Ok(Qualification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Qualification: {ex.Message}");
            }
        }

        #endregion
    }
}
