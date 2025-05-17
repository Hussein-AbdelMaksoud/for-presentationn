using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Data.Entities;
using Server.Models.DTOs.Employee;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        string[] includes = new string[]
        {
            "Department", "SubAd", "JobGroup", "JobSubGroup", "JobName", "ExistaceCase", "NonExistanceType",
            "HealthState",
            "Governrate", "SocialState", "Faculty", "JobDegredationData.FincialDegree"


        };

        private readonly IUnitOfWork _unitOfWork;
        private readonly EmployeeProfile _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, EmployeeProfile mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _unitOfWork.Employees.GetAllAsyncNoTracking(
                x => !x.IsDeleted, // Filter condition (none in this case)
                includes, // Include related entities
                true, // Use AsNoTracking
                query => query
                    .OrderBy(e => e.JobDegredationData
                        .Select(j => j.FincialDegree.Code)
                        .FirstOrDefault()) // Sort by FincialDegree.Code
            );

            if (!employees.Any())
                return NotFound("No Employee Data found.");

            var output = _mapper.ToDTOs(employees);
            return Ok(output);
        }


        [HttpGet("by-job-group/{jobGroupId}")]
        public async Task<IActionResult> GetEmployeesByJobGroup(int jobGroupId)
        {
            if (jobGroupId <= 0)
                return BadRequest("job Group ID must be a positive number.");
            try
            {
                var jobgroup = await _unitOfWork.JobGroups.GetByIdAsync(jobGroupId);
                if (jobgroup == null)
                    return NotFound($"Faculty with ID {jobGroupId} not found.");
                var employees = await _unitOfWork.Employees.GetAllAsyncNoTracking(
                x => x.JobGroupId == jobGroupId && !x.IsDeleted,
                includes,
                true,
                query => query
                    .OrderBy(e => e.JobGroup.Code) // Ordering by JobGroup Code
            );

                if (!employees.Any())
                {
                    return NotFound($"No employees found with Job Group ID: {jobGroupId}");
                }

                var output = _mapper.ToDTOs(employees);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving job Group with ID {jobGroupId}: {ex.Message}");
            }

        }

        [HttpGet("by-job-sub-group/{jobSubGroupId}")]
        public async Task<IActionResult> GetEmployeesByJobSubGroup(int jobSubGroupId)
        {
            if (jobSubGroupId <= 0)
                return BadRequest("Job SUb Group ID must be a positive number.");
            try
            {
                var jobSubgroup = await _unitOfWork.JobSubGroups.GetByIdAsync(jobSubGroupId);
                if (jobSubgroup == null)
                    return NotFound($"Job SUb Group with ID {jobSubGroupId} not found.");
                var employees = await _unitOfWork.Employees.GetAllAsyncNoTracking(
                    x => x.JobSubGroupId == jobSubGroupId &&!x.IsDeleted,
                    includes,
                    true,
                    query => query.OrderBy(e => e.JobSubGroup.Code) // ترتيب حسب كود المجموعة النوعية
                );

                if (!employees.Any())
                    return NotFound($"No Employees found with Job Sub Group ID: {jobSubGroupId}");

                var output = _mapper.ToDTOs(employees);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Job Sub Group with ID {jobSubGroupId}: {ex.Message}");
            }
        }

        [HttpGet("by-job-name/{jobNameId}")]
        public async Task<IActionResult> GetEmployeesByJobName(int jobNameId)
        {
            if (jobNameId <= 0)
                return BadRequest("Job Name ID must be a positive number.");
            try
            {
                var jobName = await _unitOfWork.JobNames.GetByIdAsync(jobNameId);
                if (jobName == null)
                    return NotFound($"Job Name with ID {jobNameId} not found.");
             
                var employees = await _unitOfWork.Employees.GetAllAsyncNoTracking(
                    x => x.JobNameId == jobNameId && !x.IsDeleted,
                    includes,
                    true,
                    query => query.OrderBy(e => e.JobName.Code) // ترتيب حسب كود مسمى الوظيفة
                );

                if (!employees.Any())
                    return NotFound($"No Employees found with Job Name ID: {jobNameId}");

                var output = _mapper.ToDTOs(employees);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Job Name with ID {jobNameId}: {ex.Message}");
            }
        }

        [HttpGet("Employee/ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Invalid Name. Please provide a valid Name.");

            name = name.Trim();

            try
            {
                var employees = await _unitOfWork.Employees.GetAllAsyncNoTracking(e => e.Name.ToLower().Contains(name.ToLower()) && !e.IsDeleted, includes);

                if (!employees.Any())
                {
                    return NotFound($"No Employee Data found for Name '{name}'.");
                }

                var output = _mapper.ToDTOs(employees);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Employee for Name '{name}': {ex.Message}");
            }
        }



        [HttpGet("GetByFaculty/{facultyId}")]
        public async Task<IActionResult> GetByFacultyId(int facultyId)
        {
            if (facultyId <= 0)
                return BadRequest("Faculty ID must be a positive number.");
            try
            {
                var faculty = await _unitOfWork.Faculties.GetByIdAsync(facultyId);
                if (faculty == null)
                    return NotFound($"Faculty with ID {facultyId} not found.");

                var employees = await _unitOfWork.Employees.GetAllAsyncNoTracking(
                    e => e.FacultyId == facultyId && !e.IsDeleted,
                    includes,
                    true, // No Tracking
                    query => query.OrderBy(e => e.Faculty.Code)
                );

                if (!employees.Any())
                {
                    return NotFound($"No Employee Data found for FacultyId '{facultyId}'.");
                }

                var output = _mapper.ToDTOs(employees);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Faculty with ID {facultyId}: {ex.Message}");
            }
        }



        [HttpGet("Employee/{nationalId}")]
        public async Task<IActionResult> GetByNationalId(string nationalId)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
                return BadRequest("Invalid NationalId. Please provide a valid NationalId.");

            nationalId = nationalId.Trim();

            try
            {

                var employees =
                    await _unitOfWork.Employees.GetAllAsyncNoTracking(e => e.NationalId == nationalId && !e.IsDeleted, includes);

                if (!employees.Any())
                {
                    return NotFound($"No Employee Data found for NationalId '{nationalId}'.");
                }

                var output = _mapper.ToDTOs(employees);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"An error occurred while retrieving Employee for NationalId '{nationalId}': {ex.Message}");
            }
        }


        [HttpGet("EmployeesWithCurrentDegree")]
        public async Task<IActionResult> GetEmployeesWithCurrentDegree()
        {

            var employees = await _unitOfWork.Employees.GetAllAsyncNoTracking(
                e => e.JobDegredationData.Any(j => j.CurrentDegree) && !e.IsDeleted,
                includes,
                true,
                query => query.OrderBy(e => e.JobDegredationData
                    .Where(j => j.CurrentDegree)
                    .Select(j => j.FincialDegree.Code)
                    .FirstOrDefault())
            );


            if (!employees.Any())
                return NotFound("لا يوجد موظفين لديهم CurrentDegree = True.");


            var output = _mapper.ToDTOs(employees);
            return Ok(output);
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Employee data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Check if National ID already exists
                var nationalIdExists = await _unitOfWork.Employees.GetFirstOrDefaultAsync(e => e.NationalId == employeeDto.NationalId);
                if (nationalIdExists != null)
                {
                    return Conflict("An employee with this National ID already exists.");
                }

                Employee employee = _mapper.ToEntity(employeeDto);
                if (employee == null)
                {
                    return BadRequest("Invalid employee data.");
                }

                JobDegredationData JD = _mapper.ToJobDegredationData(employeeDto);
                if (JD == null)
                {
                    return BadRequest("Invalid job degradation data.");
                }


                {
                    await _unitOfWork.Employees.AddAsync(employee);
                    await _unitOfWork.SaveAsync();


                    await _unitOfWork.JobDegredationDatas.AddAsync(JD);
                    await _unitOfWork.SaveAsync();


                }

                return Ok("Created Successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }










        /// <summary>
        /// 
        /// 
        /// Soft-Delete
        ///
        /// 
        /// </summary>

        [HttpPut("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            try
            {
                var emp = await _unitOfWork.Employees.FindAsync(a => a.NationalId == id);
                emp.First().IsDeleted = true;
                emp.First().DeleteTime = DateTime.Now;
                await _unitOfWork.SaveAsync();
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error Ocured while deleting the employee: {ex.Message}");
            }
        }

    }
}
