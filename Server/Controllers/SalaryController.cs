using Microsoft.AspNetCore.Mvc;
using Server.Mapping.Profile;
using Server.Models.DTOs.Salary;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SalaryProfile profile;

        public SalaryController(IUnitOfWork unitOfWork, SalaryProfile profile)
        {
            _unitOfWork = unitOfWork;
            this.profile = profile;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var entitys = await _unitOfWork.Salaries.GetAllAsync();

                if (entitys == null || !entitys.Any())
                {
                    return NotFound("No Salaries  Found.!");

                }

                var getDTO = profile.SalaryList_To_GetSalaryDTOList(entitys);
                return Ok(getDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Error Occured {ex.Message}");
            }
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("!Invalid Id.");
            try
            {
                var entity = await _unitOfWork.Salaries.GetByIdAsync(id);

                if (entity == null)
                    return NotFound($"!Salary With Id {id} NOT FOUND.");

                var GetDTO = profile.Salary_To_GetSalaryDTO(entity);
                return Ok(GetDTO);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An Error Occured {e.Message}");
            }
        }

        [HttpGet("Employee/{empId}")]
        public async Task<IActionResult> GetSalaryByEmployee(string empId)
        {
            if (string.IsNullOrWhiteSpace(empId))
                return BadRequest("Invalid Employee ID. Please provide a valid EmpId.");

            var employeeExists = await _unitOfWork.Employees.FindAsync(e => e.NationalId == empId);
            if (!employeeExists.Any())
                return NotFound($"Employee with ID {empId} not found.");

            var salaries = await _unitOfWork.Salaries.FindAsync(x => x.EmpId == empId);

            if (!salaries.Any())
                return NotFound($"No salary records found for employee {empId}.");

            var salaryDTOs = profile.SalaryList_To_GetSalaryDTOList(salaries);
            return Ok(salaryDTOs);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSalaryDTO salaryDTO)
        {
            if (salaryDTO == null)
                return BadRequest("Salary data is Required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // التحقق من وجود الموظف
                var employeeExists = await _unitOfWork.Employees
                    .FindAsync(e => e.NationalId == salaryDTO.EmpId);

                if (!employeeExists.Any())
                    return NotFound("Employee not found. Please provide a valid EmpId.");

                var entity = profile.SalaryDTO_To_Salary(salaryDTO);

                await _unitOfWork.Salaries.AddAsync(entity);
                await _unitOfWork.SaveAsync();

                return Ok(salaryDTO);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while creating: {e.Message}");
            }

        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateSalaryDTO salaryDTO)
        {
            if (salaryDTO == null)
                return BadRequest("Salary data is Required.");

            if (id <= 0)
                return BadRequest("!Invalid Id.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // التحقق من وجود الموظف
                var employeeExists = await _unitOfWork.Employees
                    .FindAsync(e => e.NationalId == salaryDTO.EmpId);

                if (!employeeExists.Any())
                    return NotFound("Employee not found. Please provide a valid EmpId.");

                var entity = await _unitOfWork.Salaries.GetByIdAsync(id);

                if (entity == null)
                    return BadRequest($"Salary With id {id} not found. ");

                entity.EmpId = salaryDTO.EmpId;
                entity.Notes = salaryDTO.Notes;
                entity.All_Mok = salaryDTO.All_Mok;
                entity.Badalat_Okhra = salaryDTO.Badalat_Okhra;
                entity.All_Badalt = salaryDTO.All_Badalt;
                entity.MoKafat_Emt7anat = salaryDTO.MoKafat_Emt7anat;
                entity.Hafez_Thabet = salaryDTO.Hafez_Thabet;
                entity.M_asasy30 = salaryDTO.M_asasy30;
                entity.Hafez_Taawedy = salaryDTO.Hafez_Taawedy;
                entity.Agr_Mokamel = salaryDTO.Agr_Mokamel;
                entity.Agr_Wasefy = salaryDTO.Agr_Wasefy;
                entity.Code = salaryDTO.Code;
                entity.Id = id;

                await _unitOfWork.SaveAsync();

                return Ok(salaryDTO);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while Updating: {e.Message}");
            }



        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Invalid Id");

            try
            {
                var entity = await _unitOfWork.Salaries.GetByIdAsync(id);

                if (entity == null)
                    return NotFound($"Salary with Id {id} not found.");

                _unitOfWork.Salaries.Delete(entity.Id);
                await _unitOfWork.SaveAsync();

                return Ok($"Salary wIth ID {id} Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while Deleting: {e.Message}");

            }
        }


    }
}

