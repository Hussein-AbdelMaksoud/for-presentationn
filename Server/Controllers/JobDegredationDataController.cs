using Microsoft.AspNetCore.Mvc;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;
using Server.Models.DTOs.JobDegredationData;
using Server.Mapping.Profile;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobDegredationDataController : ControllerBase
    {
        // Declare the UnitOfWork interface to be used for CRUD operations
        private readonly IUnitOfWork _unitOfWork;
        private readonly JobDegredationDataProfile _mapper;

        // Initialize the controller with dependency injection for UnitOfWork
        public JobDegredationDataController(IUnitOfWork unitOfWork, JobDegredationDataProfile mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<JobDegredationData> jobDegredationDatas = await _unitOfWork.JobDegredationDatas.GetAllAsync();

                if (jobDegredationDatas == null || !jobDegredationDatas.Any())
                    return NotFound("No JobDegredationData found.");

                // Use Mapperly to convert the entity list to a DTO list.
                var dtos = _mapper.JobDegList_ToJobDegDTOList(jobDegredationDatas);
                return Ok(dtos);
            }
            catch
            {
                return StatusCode(500, "Error retrieving JobDegredationData.");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id.");

            try
            {
                var jobDegredationData = await _unitOfWork.JobDegredationDatas.GetByIdAsync(id);

                if (jobDegredationData == null)
                    return NotFound($"Job Degredation Data with ID {id} not found.");
                JobDegredationDataDTO dto = _mapper.JobDeg_ToJobDegDTO(jobDegredationData);
                return Ok(dto);
            }
            catch
            {
                return StatusCode(500, "Error retrieving jobDegData.");
            }

        }

        [HttpGet("Employee/{empId}")]
        public async Task<IActionResult> GetJobDegradationByEmpID(string empId)
        {
            if (string.IsNullOrWhiteSpace(empId))
                return BadRequest("Invalid Employee ID. Please provide a valid EmpId.");

            // التحقق من وجود الموظف
            var employeeExists = await _unitOfWork.Employees.FindAsync(e => e.NationalId == empId);
            if (!employeeExists.Any())
                return NotFound($"Employee with ID {empId} not found.");

            // تضمين العلاقات المهمة مثل الدرجة المالية ونوع الوظيفة
            var includes = new string[] { "Employee","FincialDegree", "JobType" };

            // جلب بيانات التدرج الوظيفي لهذا الموظف
            var jobDegradations = await _unitOfWork.JobDegredationDatas.FindAsync(x => x.EmpId == empId, includes);

            if (!jobDegradations.Any())
                return NotFound($"No job degradation records found for employee {empId}.");

            // تحويل البيانات إلى DTO وإرجاعها
            var output = _mapper.JobDegList_ToJobDegDTOList(jobDegradations);
            return Ok(output);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobDegredationDataDTO jobDegDto)
        {
            if (jobDegDto == null)
                return BadRequest("Job Degredation Data data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // التحقق من أن StartDate أصغر من EndDate
            if (jobDegDto.JobStartDate.HasValue && jobDegDto.JobEndDate.HasValue && jobDegDto.JobStartDate >= jobDegDto.JobEndDate)
            {
                return BadRequest("StartDate must be before JobEndDate.");
            }
          
            // التحقق من أن DecisionDate ليس بعد StartDate
            if (jobDegDto.DecisionDate.HasValue && jobDegDto.JobStartDate.HasValue && jobDegDto.DecisionDate > jobDegDto.JobStartDate)
            {
                return BadRequest("DecisionDate cannot be after JobStartDate.");
            }

            //التحقق ان fincialdegreeDate ليس قبل الstartdate 
            if (jobDegDto.FincialDegreeDate <= jobDegDto.JobStartDate)
            {
                return BadRequest("FincialDegreeDate can not be before JobStartDate.");
            }
            
            //التحقق ان fincialdegreeDate ليس قبل ال Decisiondate 
            if (jobDegDto.FincialDegreeDate <= jobDegDto.DecisionDate)
            {
                return BadRequest("FincialDegreeDate can not be before DecisionDate.");
            }
           
            //التحقق ان fincialdegreeDate ليس بعد الEndDate 
            if (jobDegDto.FincialDegreeDate > jobDegDto.JobEndDate)
            {
                return BadRequest("FincialDegreeDate can not be after JobEndtDate.");
            }

            // Ensure CurrentDegree remains true
            if (!jobDegDto.CurrentDegree)
                return BadRequest("CurrentDegree must be true.");

            try
            {
                // التحقق من وجود الموظف
                var employeeExists = await _unitOfWork.Employees
                    .FindAsync(e => e.NationalId == jobDegDto.EmpId);

                if (!employeeExists.Any())
                    return NotFound("Employee not found. Please provide a valid EmpId.");

                // ✅ التحقق من أن FincialDegreeId موجود في الجدول المرتبط
                if (jobDegDto.FincialDegreeId > 0)
                {
                    var fincialDegreeExists = await _unitOfWork.FincialDegrees.GetByIdAsync(jobDegDto.FincialDegreeId);
                    if (fincialDegreeExists == null)
                        return BadRequest($"No FincialDegree found with ID {jobDegDto.FincialDegreeId}.FincialDegreeId must match an existing record in the database.");
                }
                else
                {
                    //fin Deg Type ID can't be zero
                    return BadRequest("FincialDegreeId cannot be zero.");
                }

                // ✅ التحقق من أن JobTypeId موجود في الجدول المرتبط
                if (jobDegDto.JobTypeId.HasValue)
                {
                    //check that jobType can't be zero
                    if (jobDegDto.JobTypeId == 0)
                        return BadRequest("JobTypeId can not be zero.");

                    var jobTypeExists = await _unitOfWork.JobTypes.GetByIdAsync(jobDegDto.JobTypeId.Value);
                    if (jobTypeExists == null)
                        return BadRequest($"No JobType found with ID {jobDegDto.JobTypeId.Value}.JobTypeId must match an existing record in the database.");
                }
                // Map the DTO to the entity.

                JobDegredationData jobDegEntity = _mapper.CreateJobDegDto_TOJobDeg(jobDegDto);
                jobDegEntity.CurrentDegree = true;

                await _unitOfWork.JobDegredationDatas.AddAsync(jobDegEntity);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetByID), new { id = jobDegEntity.Id }, jobDegDto);
            }
            catch
            {
                return StatusCode(500, "Error creating Job Deg Data.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateJobDegredationDataDTO jobDegDto)
        {
            if (jobDegDto == null)
                return BadRequest("Job Deg Data data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
         
            // التحقق من أن StartDate أصغر من EndDate
            if (jobDegDto.JobStartDate.HasValue && jobDegDto.JobEndDate.HasValue && jobDegDto.JobStartDate >= jobDegDto.JobEndDate)
            {
                return BadRequest("StartDate must be before JobEndDate.");
            }

            // التحقق من أن DecisionDate ليس بعد StartDate
            if (jobDegDto.DecisionDate.HasValue && jobDegDto.JobStartDate.HasValue && jobDegDto.DecisionDate > jobDegDto.JobStartDate)
            {
                return BadRequest("DecisionDate can not be after JobStartDate.");
            }
            
            //التحقق ان fincialdegreeDate ليس قبل الstartdate 
            if (jobDegDto.FincialDegreeDate <= jobDegDto.JobStartDate)
            {
                return BadRequest("FincialDegreeDate can not be before JobStartDate.");
            }

            //التحقق ان fincialdegreeDate ليس قبل ال Decisiondate 
            if (jobDegDto.FincialDegreeDate <= jobDegDto.DecisionDate)
            {
                return BadRequest("FincialDegreeDate can not be before DecisionDate.");
            }

            //التحقق ان fincialdegreeDate ليس بعد الEndDate 
            if (jobDegDto.FincialDegreeDate > jobDegDto.JobEndDate)
            {
                return BadRequest("FincialDegreeDate can not be after JobEndtDate.");
            }
            // Ensure CurrentDegree remains true
            if (!jobDegDto.CurrentDegree)
                return BadRequest("CurrentDegree must be true.");

            try
            {

                // التحقق من وجود الموظف
                var employeeExists = await _unitOfWork.Employees
                    .FindAsync(e => e.NationalId == jobDegDto.EmpId);

                if (!employeeExists.Any())
                    return NotFound("Employee not found. Please provide a valid EmpId.");

                var jobDeg = await _unitOfWork.JobDegredationDatas.GetByIdAsync(id);

                if (jobDeg == null)
                    return NotFound($"JobDegData with ID {id} not found.");

                // ✅ التحقق من أن FincialDegreeId موجود في الجدول المرتبط
                if (jobDegDto.FincialDegreeId > 0)
                {
                  
                    var fincialDegreeExists = await _unitOfWork.FincialDegrees.GetByIdAsync(jobDegDto.FincialDegreeId);
                    if (fincialDegreeExists == null)
                        return BadRequest($"No FincialDegree found with ID {jobDegDto.FincialDegreeId}.FincialDegreeId must match an existing record in the database.");
                }
                else
                {                   
                    //fin Deg Type ID can't be zero
                    return BadRequest("FincialDegreeId cannot be zero.");
                }
               
                // ✅ التحقق من أن JobTypeId موجود في الجدول المرتبط
                if (jobDegDto.JobTypeId.HasValue)
                {
                    //check that finDegType can't be zero
                    if (jobDegDto.JobTypeId == 0)
                        return BadRequest("JobTypeId cannot be zero.");

                    var jobTypeExists = await _unitOfWork.JobTypes.GetByIdAsync(jobDegDto.JobTypeId.Value);
                    if (jobTypeExists == null)
                        return BadRequest($"No JobType found with ID {jobDegDto.JobTypeId.Value}.JobTypeId must match an existing record in the database.");
                }
                jobDeg.Code = jobDegDto.Code;
                jobDeg.CurrentDegree = true; 
                jobDeg.JobEndDate = jobDegDto.JobEndDate;
                jobDeg.JobStartDate = jobDegDto.JobStartDate;
                jobDeg.FincialDegreeDate= jobDegDto.FincialDegreeDate;
                jobDeg.EmpId = jobDegDto.EmpId;
                jobDeg.FincialDegreeId = jobDegDto.FincialDegreeId;
                jobDeg.DecisionDate = jobDegDto.DecisionDate;
                jobDeg.JobName = jobDegDto.JobName;
                jobDeg.JobTypeId = jobDegDto.JobTypeId;
                jobDeg.Notes = jobDegDto.Notes;
                await _unitOfWork.SaveAsync();

                return Ok(jobDegDto);
            }
            catch
            {
                return StatusCode(500, "Error updating job Degredataion Data.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            try
            {
                var jobDeg = await _unitOfWork.JobDegredationDatas.GetByIdAsync(id);

                if (jobDeg == null)
                    return NotFound($"Job Degredation Data with ID {id} not found.");


                _unitOfWork.JobDegredationDatas.Delete(jobDeg.Id);
                await _unitOfWork.SaveAsync();

                return Ok($"Job Degredation Data with ID {id} deleted Successfully");
            }
            catch
            {
                return StatusCode(500, "Error deleting job Degredation Data.");
            }
        }
    }
}
