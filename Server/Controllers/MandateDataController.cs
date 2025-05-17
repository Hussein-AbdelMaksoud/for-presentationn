using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.MandateData;
using Server.Services.UnitOfWork.Interfaces;
using Server.Mapping.Profile;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MandateDataController : ControllerBase
    {
        string[] relatedEntities = new string[] { "Employee", "MandateType", "Faculty" };

        private readonly IUnitOfWork _unitOfWork;
        private readonly MandateDataProfile _profile;

        public MandateDataController(IUnitOfWork unitOfWork, MandateDataProfile profile)
        {
            _unitOfWork = unitOfWork;
            _profile = profile;
        }

        #region Endpoints


        /// <summary>
        /// GET: api/MandateData
        /// - **Description**: Retrieves all MandateData records with related Employee data from the database.
        /// - **URL**: `GET api/MandateData`
        /// - **Response**: A list of all MandateData records.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                var mandateDataList = await _unitOfWork.MandateDatas
                    .FindAsync(x => true, relatedEntities);

                if (!mandateDataList.Any())
                    return NotFound("No Mandate Data found.");

                var mandateDataDtos = _profile.ToDTOs(mandateDataList);
                return Ok(mandateDataDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Mandate Data: {ex.Message}");
            }
        }



        /// <summary>
        /// Get MandateData by ID.
        /// URL: GET api/MandateDatae/{id}
        /// </summary>
        /// <param name="id">MandateData ID</param>
        /// <returns>MandateDataDTO</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. Please provide a valid positive ID.");

            try
            {

                var mandateData = await _unitOfWork.MandateDatas
                    .FindAsync(md => md.Id == id, relatedEntities);

                var result = mandateData.FirstOrDefault();

                if (result == null)
                    return NotFound($"Mandate Data with ID {id} not found.");

                var mandateDataDto = _profile.ToDTO(result);
                return Ok(mandateDataDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the Mandate Data: {ex.Message}");
            }
        }


        /// <summary>
        /// Get Mandate Data by Employee ID (EmpId).
        /// URL: GET api/MandateData/ByEmpId/{empId}
        /// </summary>
        /// <param name="empId">Employee National ID</param>
        /// <returns>List of MandateDataDTO</returns>
        [HttpGet("Employee/{empId}")]
        public async Task<IActionResult> GetByEmpId(string empId)
        {
            if (string.IsNullOrWhiteSpace(empId))
                return BadRequest("Invalid EmpId. Please provide a valid EmpId.");

            empId = empId.Trim();

            try
            {

                var mandateDataList = await _unitOfWork.MandateDatas
                    .FindAsync(md => md.EmpId == empId, relatedEntities);

                if (!mandateDataList.Any())
                    return NotFound($"No Mandate Data found for EmpId '{empId}'.");

                var mandateDataDtos = _profile.ToDTOs(mandateDataList);
                return Ok(mandateDataDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Mandate Data for EmpId '{empId}': {ex.Message}");
            }
        }



        /// <summary>
        /// Create a new Mandate Data.
        /// URL: POST api/MandateData
        /// </summary>
        /// <param name="createDto">CreateMandateDataDTO object containing the new Mandate Data details</param>
        /// <returns>Created MandateDataDTO with related entities</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMandateDataDTO createDto)
        {
            if (createDto == null)
                return BadRequest("Mandate Data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // التحقق من أن StartDate أصغر من EndDate
            if (createDto.StartDate.HasValue && createDto.EndDate.HasValue && createDto.StartDate > createDto.EndDate)
            {
                return BadRequest("StartDate must be before EndDate.");
            }

            // التحقق من أن DecisionDate ليس بعد StartDate
            if (createDto.DecisionDate.HasValue && createDto.StartDate.HasValue && createDto.DecisionDate > createDto.StartDate)
            {
                return BadRequest("DecisionDate cannot be after StartDate.");
            }


            try
            {
                
                var employeeExists = await _unitOfWork.Employees
                    .FindAsync(e => e.NationalId == createDto.EmpId);

                if (!employeeExists.Any())
                    return NotFound("Employee not found. Please provide a valid EmpId.");

                // التحقق من نوع المأمورية
                if (createDto.MandateTypeId.HasValue)
                {
                    var mandateTypeExists = await _unitOfWork.MandateTypes
                        .GetByIdAsync(createDto.MandateTypeId.Value);

                    if (mandateTypeExists == null)
                        return NotFound("Mandate Type not found. Please provide a valid MandateTypeId.");
                }

                // التحقق من الكلية
                if (createDto.FacultyId.HasValue)
                {
                    var facultyExists = await _unitOfWork.Faculties
                        .GetByIdAsync(createDto.FacultyId.Value);

                    if (facultyExists == null)
                        return NotFound("Faculty not found. Please provide a valid FacultyId.");
                }

                // التحقق من IsMandated
                if (createDto.IsMandated)
                {
                    // التأكد من عدم وجود سجل نشط آخر لنفس الموظف
                    var activeMandate = await _unitOfWork.MandateDatas
                        .FindAsync(md => md.EmpId == createDto.EmpId && md.IsMandated);

                    if (activeMandate.Any())
                        return Conflict("An active Mandate already exists for this employee.");
                }

                var mandateData = _profile.ToCreateEntity(createDto);

                await _unitOfWork.MandateDatas.AddAsync(mandateData);
                await _unitOfWork.SaveAsync();

                var mandateDataWithRelations = (await _unitOfWork.MandateDatas
                    .FindAsync(md => md.Id == mandateData.Id, relatedEntities))
                    .FirstOrDefault();

                if (mandateDataWithRelations == null)
                    return StatusCode(500, "Mandate Data retrieval after save returned null.");

                var mandateDataDto = _profile.ToDTO(mandateDataWithRelations);

                return CreatedAtAction(nameof(GetById), new { id = mandateDataDto.Id }, mandateDataDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Mandate Data: {ex.Message}");
            }
        }

        /// <summary>
        /// Update an existing Mandate Data.
        /// URL: PUT api/MandateData/{id}
        /// </summary>
        /// <param name="id">Mandate Data ID</param>
        /// <param name="updateDto">CreateMandateDataDTO object</param>
        /// <returns>Updated MandateDataDTO</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateMandateDataDTO updateDto)
        {
            if (updateDto == null)
                return BadRequest("Mandate Data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // التحقق من أن StartDate أصغر من EndDate
            if (updateDto.StartDate.HasValue && updateDto.EndDate.HasValue && updateDto.StartDate > updateDto.EndDate)
            {
                return BadRequest("StartDate must be before EndDate.");
            }

            // التحقق من أن DecisionDate ليس بعد StartDate
            if (updateDto.DecisionDate.HasValue && updateDto.StartDate.HasValue && updateDto.DecisionDate > updateDto.StartDate)
            {
                return BadRequest("DecisionDate cannot be after StartDate.");
            }


            try
            {
                var existingMandateData = await _unitOfWork.MandateDatas
                    .FindAsync(md => md.Id == id, relatedEntities);

                var mandateData = existingMandateData.FirstOrDefault();

                if (mandateData == null)
                    return NotFound($"Mandate Data with ID {id} not found.");

                // التحقق من وجود الموظف
                var employeeExists = await _unitOfWork.Employees
                    .FindAsync(e => e.NationalId == updateDto.EmpId);

                if (!employeeExists.Any())
                    return NotFound("Employee not found. Please provide a valid EmpId.");

                // التحقق من نوع المأمورية
                if (updateDto.MandateTypeId.HasValue)
                {
                    var mandateTypeExists = await _unitOfWork.MandateTypes
                        .GetByIdAsync(updateDto.MandateTypeId.Value);

                    if (mandateTypeExists == null)
                        return NotFound("Mandate Type not found. Please provide a valid MandateTypeId.");
                }

                // التحقق من الكلية
                if (updateDto.FacultyId.HasValue)
                {
                    var facultyExists = await _unitOfWork.Faculties
                        .GetByIdAsync(updateDto.FacultyId.Value);

                    if (facultyExists == null)
                        return NotFound("Faculty not found. Please provide a valid FacultyId.");
                }

                // التحقق من IsMandated
                if (updateDto.IsMandated && !mandateData.IsMandated)
                {
                    var existingMandate = await _unitOfWork.MandateDatas
                        .FindAsync(md => md.EmpId == updateDto.EmpId && md.IsMandated && md.Id != id);

                    if (existingMandate.Any())
                    {
                        return Conflict("This employee already has an active mandate. Only one active mandate is allowed per employee.");
                    }
                }

                // تحديث الحقول
                mandateData.Code = updateDto.Code;
                mandateData.EmpId = updateDto.EmpId;
                mandateData.MandateTypeId = updateDto.MandateTypeId;
                mandateData.FacultyId = updateDto.FacultyId;
                mandateData.IsMandated = updateDto.IsMandated;
                mandateData.Geha = updateDto.Geha;
                mandateData.MandateJob = updateDto.MandateJob;
                mandateData.DecisionNo = updateDto.DecisionNo;
                mandateData.DecisionDate = updateDto.DecisionDate;
                mandateData.StartDate = updateDto.StartDate;
                mandateData.EndDate = updateDto.EndDate;

                _unitOfWork.MandateDatas.Update(mandateData);
                await _unitOfWork.SaveAsync();

                var updatedMandateData = (await _unitOfWork.MandateDatas
                    .FindAsync(md => md.Id == id, relatedEntities))
                    .FirstOrDefault();

                if (updatedMandateData == null)
                    return StatusCode(500, "Mandate Data retrieval after update returned null.");

                var updatedDto = _profile.ToDTO(updatedMandateData);
                return Ok(updatedDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Mandate Data: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a Mandate Data by ID.
        /// URL: DELETE api/MandateData/{id}
        /// </summary>
        /// <param name="id">Mandate Data ID</param>
        /// <returns>No content on success</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var mandateData = await _unitOfWork.MandateDatas.GetByIdAsync(id);
                if (mandateData == null)
                    return NotFound($"Mandate Data with ID {id} not found.");

                _unitOfWork.MandateDatas.Delete(mandateData.Id);
                await _unitOfWork.SaveAsync();

                return Ok("Mandate Data deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Mandate Data: {ex.Message}");
            }
        }


        #endregion
    }
}
