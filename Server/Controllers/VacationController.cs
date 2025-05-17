using Microsoft.AspNetCore.Mvc;
using Server.Mapping.Profile;
using Server.Models.DTOs.FincialDegree;
using Server.Models.DTOs.Vacation;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly VacationProfile profile;

        public VacationController(IUnitOfWork unitOfWork, VacationProfile profile)
        {
            _unitOfWork = unitOfWork;
            this.profile = profile;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var entitys = await _unitOfWork.Vacations.GetAllAsync();

                if (entitys == null || !entitys.Any())
                {
                    return NotFound("No Vacations  Found.!");

                }

                var getDTO = profile.VacationList_To_GetVacationDTOList(entitys);
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
                return BadRequest("!Invalid Id.Id can't be less or equal to zero");
            try
            {
                var entity = await _unitOfWork.Vacations.GetByIdAsync(id);

                if (entity == null)
                    return NotFound($"!Vacation with Id {id} not found.");

                var GetDTO = profile.Vacation_To_GetVacationDTO(entity);
                return Ok(GetDTO);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An Error Occured {e.Message}");
            }
        }


        [HttpGet("Employee/{empId}")]
        public async Task<IActionResult> GetVacationsByEmployee(string empId)
        {
            if (string.IsNullOrWhiteSpace(empId))
                return BadRequest("Invalid Employee ID. Please provide a valid EmpId.");

            var employeeExists = await _unitOfWork.Employees.FindAsync(e => e.NationalId == empId);
            if (!employeeExists.Any())
                return NotFound($"Employee with ID {empId} not found.");

            var vacations = await _unitOfWork.Vacations.FindAsync(x => x.EmpId == empId);

            if (!vacations.Any())
                return NotFound($"No vacation records found for employee {empId}.");

            var output= profile.VacationList_To_GetVacationDTOList(vacations);
            return Ok(output);
        }




        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVacationDTO vacationDTO)
        {
            if (vacationDTO == null)
                return BadRequest("Vacation data is Required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            // التحقق من أن StartDate أصغر من EndDate

            if (vacationDTO.StartDate.HasValue && vacationDTO.EndDate.HasValue && vacationDTO.StartDate >= vacationDTO.EndDate)
            {
                return BadRequest("StartDate must be before EndDate.");
            }

            // التحقق من أن DecisionDate ليس بعد StartDate
            if (vacationDTO.DecisionDate.HasValue && vacationDTO.StartDate.HasValue && vacationDTO.DecisionDate > vacationDTO.StartDate)
            {
                return BadRequest("DecisionDate cannot be after StartDate.");
            }
           
            // التحقق من أن startdate  ليس بعد returndate
            if (vacationDTO.StartDate.HasValue && vacationDTO.ReturnDate.HasValue && vacationDTO.StartDate >= vacationDTO.ReturnDate)
            {
                return BadRequest("ReturnDate must be after StartDate.");
            }

            // التحقق من أن EndDate  ليس بعد returndate
            if (vacationDTO.EndDate.HasValue && vacationDTO.ReturnDate.HasValue && vacationDTO.EndDate < vacationDTO.ReturnDate)
            {
                return BadRequest("ReturnDate can not be after EndDate,ReturnDate must be same or before to EndDate.");
            }
            try
            {// التحقق من وجود الموظف
                var employeeExists = await _unitOfWork.Employees
                    .FindAsync(e => e.NationalId ==vacationDTO.EmpId);

                if (!employeeExists.Any())
                    return NotFound("Employee not found. Please provide a valid EmpId.");

                //check that vacationType can't be zero
                if (vacationDTO.VacationTypeId == 0)
                    return BadRequest("VacationTypeId cannot be zero.");

                var VacationTypeExist = await _unitOfWork.VacationTypes.GetByIdAsync(vacationDTO.VacationTypeId.Value);
                if (VacationTypeExist == null)
                    return BadRequest("Vacation Type Not Found.VacationTypeId must match an existing record in the database");

                // **`Ended = true` إذا كانت الجديدة>= ** `Ended = false`جعل باقي الإجازات 
                if (vacationDTO.Ended)
                {
                    var employeeVacations = await _unitOfWork.Vacations.FindAsync(v => v.EmpId == vacationDTO.EmpId);
                    foreach (var vacation in employeeVacations)
                    {
                        vacation.Ended = false;
                    }
                }
                var entity = profile.VacationDTO_To_Vacation(vacationDTO);

                await _unitOfWork.Vacations.AddAsync(entity);
                await _unitOfWork.SaveAsync();

                return Ok(vacationDTO);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while creating: {e.Message}");
            }

        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateVacationDTO vacationDTO)
        {
            if (vacationDTO == null)
                return BadRequest("Vacation data is Required.");

            if (id <= 0)
                return BadRequest("!Invalid Id.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // التحقق من أن StartDate أصغر من EndDate

            if (vacationDTO.StartDate.HasValue && vacationDTO.EndDate.HasValue && vacationDTO.StartDate >= vacationDTO.EndDate)
            {
                return BadRequest("StartDate must be before or same to EndDate.");
            }

            // التحقق من أن DecisionDate ليس بعد StartDate
            if (vacationDTO.DecisionDate.HasValue && vacationDTO.StartDate.HasValue && vacationDTO.DecisionDate > vacationDTO.StartDate)
            {
                return BadRequest("DecisionDate cannot be after StartDate.");
            }
          
            // التحقق من أن startdate  ليس بعد returndate
            if (vacationDTO.StartDate.HasValue && vacationDTO.ReturnDate.HasValue && vacationDTO.StartDate >= vacationDTO.ReturnDate)
            {
                return BadRequest("ReturnDate must be after StartDate.");
            }

            //التحقق ان returndate ليس بعد الendDate
            if (vacationDTO.EndDate.HasValue && vacationDTO.ReturnDate.HasValue && vacationDTO.EndDate < vacationDTO.ReturnDate)
            {
                return BadRequest("ReturnDate can not be after EndDate,ReturnDate must be same or before to EndDate");
            }
            try
            {
                // التحقق من وجود الموظف
                var employeeExists = await _unitOfWork.Employees
                    .FindAsync(e => e.NationalId == vacationDTO.EmpId);

                if (!employeeExists.Any())
                    return NotFound("Employee not found. Please provide a valid EmpId.");

                //check that vacationType can't be zero
                if (vacationDTO.VacationTypeId == 0)
                    return BadRequest("VacationTypeId cannot be zero.");

                var entity = await _unitOfWork.Vacations.GetByIdAsync(id);

                if (entity == null)
                    return BadRequest($"Vacation With id {id} not found. ");

                var VacationTypeExist = await _unitOfWork.VacationTypes.GetByIdAsync(vacationDTO.VacationTypeId.Value);
                if (VacationTypeExist == null)
                    return BadRequest("Vacation Type Not Found.VacationTypeId must match an existing record in the database");
              
                // **`Ended = true` إذا كانت الجديدة>= ** `Ended = false`جعل باقي الإجازات 
                if (vacationDTO.Ended)
                {
                    var employeeVacations = await _unitOfWork.Vacations.FindAsync(v => v.EmpId == vacationDTO.EmpId);
                    foreach (var vacation in employeeVacations)
                    {
                        vacation.Ended = false;
                    }
                }

                entity.EmpId=vacationDTO.EmpId;
                entity.Notes = vacationDTO.Notes;
                entity.Code = vacationDTO.Code;
                entity.DecisionDate = vacationDTO.DecisionDate;
                entity.DecisionNo = vacationDTO.DecisionNo;
                entity.ReturnDate = vacationDTO.ReturnDate;
                entity.EndDate = vacationDTO.EndDate;
                entity.StartDate = vacationDTO.StartDate;
                entity.VacationTypeId = vacationDTO.VacationTypeId;
                entity.Place = vacationDTO.Place;
                entity.Ended = vacationDTO.Ended;
                entity.Id = id;

                await _unitOfWork.SaveAsync();

                return Ok(vacationDTO);

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
                var entity = await _unitOfWork.Vacations.GetByIdAsync(id);

                if (entity == null)
                    return NotFound($"Vacation with Id {id} not found.");

                _unitOfWork.Vacations.Delete(entity.Id);
                await _unitOfWork.SaveAsync();

                return Ok($"Vacation with ID {id} is Deleted Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while Deleting: {e.Message}");

            }
        }


    }
}
