using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Mapping.Profile;
using Server.Models.DTOs.ThanksLetter;
using Server.Models.DTOs.Vacation;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanksLetterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ThanksLetterProfile profile;

        public ThanksLetterController(IUnitOfWork unitOfWork, ThanksLetterProfile profile)
        {
            _unitOfWork = unitOfWork;
            this.profile = profile;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var entitys = await _unitOfWork.ThanksLetters.GetAllAsync();

                if (entitys == null || !entitys.Any())
                {
                    return NotFound("No ThanksLetters  Found.!");

                }

                var getDTO = profile.ThanksLetterList_To_GetThanksLetterDTOList(entitys);
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
                var entity = await _unitOfWork.ThanksLetters.GetByIdAsync(id);

                if (entity == null)
                    return NotFound($"!ThanksLetter With Id{id} NOT FOUND.");

                var GetDTO = profile.ThanksLetter_To_GetThanksLetterDTO(entity);
                return Ok(GetDTO);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An Error Occured {e.Message}");
            }
        }

        [HttpGet("Employee/{empId}")]
        public async Task<IActionResult> GetThanksLettersByEmployee(string empId)
        {
            if (string.IsNullOrWhiteSpace(empId))
                return BadRequest("Invalid Employee ID. Please provide a valid EmpId.");

            var employeeExists = await _unitOfWork.Employees.FindAsync(e => e.NationalId == empId);
            if (!employeeExists.Any())
                return NotFound($"Employee with ID {empId} not found.");

            var letters = await _unitOfWork.ThanksLetters.FindAsync(x => x.EmpId == empId);

            if (!letters.Any())
                return NotFound($"No thanks letter records found for employee {empId}.");

            var output = profile.ThanksLetterList_To_GetThanksLetterDTOList(letters);
            return Ok(output);
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateThanksLetterDTO createdDto)
        {
            if (createdDto == null)
                return BadRequest("ThanksLetter data is Required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {// التحقق من وجود الموظف
                var employeeExists = await _unitOfWork.Employees
                    .FindAsync(e => e.NationalId == createdDto.EmpId);

                if (!employeeExists.Any())
                    return NotFound("Employee not found. Please provide a valid EmpId.");


                var entity = profile.ThanksLetterDTO_To_ThanksLetter(createdDto);

                await _unitOfWork.ThanksLetters.AddAsync(entity);
                await _unitOfWork.SaveAsync();

                return Ok(createdDto);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while creating: {e.Message}");
            }

        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateThanksLetterDTO createdDto)
        {
            if (createdDto == null)
                return BadRequest("ThanksLetter data is Required.");

            if (id <= 0)
                return BadRequest("!Invalid Id.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // التحقق من وجود الموظف
                var employeeExists = await _unitOfWork.Employees
                    .FindAsync(e => e.NationalId == createdDto.EmpId);

                if (!employeeExists.Any())
                    return NotFound("Employee not found. Please provide a valid EmpId.");

                var entity = await _unitOfWork.ThanksLetters.GetByIdAsync(id);

                if (entity == null)
                    return BadRequest($"ThanksLetter With id {id} not found. ");

                entity.EmpId=createdDto.EmpId;
                entity.DecisionNo = createdDto.DecisionNo;
                entity.DecisionDate = createdDto.DecisionDate;
                entity.Geha = createdDto.Geha;
                entity.LetterName = createdDto.LetterName;
                entity.Code = createdDto.Code;
                
                entity.Id = id;

                await _unitOfWork.SaveAsync();

                return Ok(createdDto);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while updating: {e.Message}");
            }



        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Invalid Id");

            try
            {
                var entity = await _unitOfWork.ThanksLetters.GetByIdAsync(id);

                if (entity == null)
                    return NotFound($"ThanksLetters with Id {id} not found.");

                _unitOfWork.ThanksLetters.Delete(entity.Id);
                await _unitOfWork.SaveAsync();

                return Ok($"ThanksLetter with ID {id} is Deleted Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while Deleting: {e.Message}");

            }
        }


    }
}
