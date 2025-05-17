using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Mapping.Profile;
using Server.Models.DTOs.Penalty;
using Server.Models.DTOs.YearReport;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class PenaltyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PenaltyProfile profile;

        public PenaltyController(IUnitOfWork unitOfWork, PenaltyProfile profile)
        {
            _unitOfWork = unitOfWork;
            this.profile = profile;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var includes = new string[] { "Employee", "PenaltyType", "PenaltyCase" };
                var entitys = await _unitOfWork.Penalties.FindAsync(x=> true , includes);

                if (entitys == null || !entitys.Any())
                {
                    return NotFound("No Penalties  Found.!");

                }

                var getDTO = profile.PenaltyList_To_GetPenaltyDTOList(entitys);
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
                var includes = new string[] { "Employee", "PenaltyType", "PenaltyCase" };

                var entity = (await _unitOfWork.Penalties.FindAsync(x=>x.Id == id , includes)).FirstOrDefault();

                if (entity == null)
                    return NotFound($"!Penaltie With Id{id} NOT FOUND.");

                var GetDTO = profile.Penalty_To_GetPenaltyDTO(entity);
                return Ok(GetDTO);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An Error Occured {e.Message}");
            }
        }


        //[HttpGet("byname/{name}")]
        //public async Task<IActionResult> GetByName(string name)
        //{
        //    if (string.IsNullOrWhiteSpace(name))
        //        return BadRequest("Invalid name.");

        //    try
        //    {


        //        var entities = await _unitOfWork.YearReports.FindAsync(e => e.Name.ToLower().Contains(name.ToLower()));

        //        if (entities == null || !entities.Any())
        //            return NotFound($"No VacationTypes found With Name{name}");

        //        var GetDTO = profile.VacationTypeList_To_GetVacationTypeDTOList(entities);
        //        return Ok(GetDTO);

        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, $"An Error Occured {e.Message}");
        //    }
        //}


        [HttpGet("Employee/{empId}")]
        public async Task<IActionResult> GetPenaltyByEmployee(string empId)
        {
            if (string.IsNullOrWhiteSpace(empId))
                return BadRequest("Invalid EmpId. Please provide a valid EmpId.");

            // Check if employee exists
            var employeeExists = await _unitOfWork.Employees.FindAsync(e => e.NationalId == empId);
            if (!employeeExists.Any())
                return NotFound("Employee not found. Please provide a valid EmpId.");

            var includes = new string[] { "Employee", "PenaltyType", "PenaltyCase" };

            var Penalties = await _unitOfWork.Penalties.FindAsync(x => x.EmpId == empId, includes);

            if (!Penalties.Any())
                return NotFound($"No Penalty Data found for employee {empId}.");

            return Ok(profile.PenaltyList_To_GetPenaltyDTOList(Penalties));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePenaltyDTO PenaltyRequestDTO)
        {
            if (PenaltyRequestDTO == null)
                return BadRequest("Penalty data is Required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var employeeExist = await _unitOfWork.Employees
                                             .FindAsync(e => e.NationalId == PenaltyRequestDTO.EmpId);
               
                if (!employeeExist.Any())
                    return NotFound("Employee not found. Please provide a valid EmpId.");

                if (PenaltyRequestDTO.PenaltyCaseId.HasValue)
                {
                    var penaltyCase = await _unitOfWork.PenaltyCases.GetByIdAsync(PenaltyRequestDTO.PenaltyCaseId.Value);

                    if (penaltyCase == null)
                        return NotFound("penalty case not found");
                }

                if (PenaltyRequestDTO.PenaltyTypeId.HasValue)
                {
                    var penaltyType = await _unitOfWork.PenaltyTypes.GetByIdAsync(PenaltyRequestDTO.PenaltyTypeId.Value);
                    if (penaltyType == null)
                        return NotFound("penalty type not found");
                }




                var entity = profile.PenaltyDTO_To_Penalty(PenaltyRequestDTO);

                await _unitOfWork.Penalties.AddAsync(entity);
                await _unitOfWork.SaveAsync();

                var includes = new string[] { "Employee", "PenaltyType", "PenaltyCase" };

                var PenaltyAllData = (       await _unitOfWork.Penalties
                                             .FindAsync(p => p.Id == entity.Id, includes))
                                             .FirstOrDefault();

                if (PenaltyAllData == null)
                    return StatusCode(500, "Penalty data After Saave returned null");

                var GetDTO = profile.Penalty_To_GetPenaltyDTO(PenaltyAllData);

                return Ok(GetDTO);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while creating: {e.Message}");
            }

        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreatePenaltyDTO PenaltyRequestDTO)
        {
            if (PenaltyRequestDTO == null)
                return BadRequest("Penalty data is Required.");

            if (id <= 0)
                return BadRequest("!Invalid Id.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var entity = await _unitOfWork.Penalties.GetByIdAsync(id);

                if (entity == null)
                    return BadRequest($"Penalty With id {id} not found. ");

                var EmployeeExist = await _unitOfWork.Employees.FindAsync(e => e.NationalId == PenaltyRequestDTO.EmpId);

                if (!EmployeeExist.Any())
                    return NotFound("Employee Not Found..");

                var PenaltyCaseExist = await _unitOfWork.PenaltyCases.GetByIdAsync(int.Parse(PenaltyRequestDTO.PenaltyCaseId.ToString()));
                
                var PenaltyTypeExist = await _unitOfWork.PenaltyTypes.GetByIdAsync(int.Parse(PenaltyRequestDTO.PenaltyTypeId.ToString()));


                if (PenaltyCaseExist == null || PenaltyTypeExist == null)
                    return BadRequest("Penalty Type or Penalty Case Not Found");

                entity.Id = id;
                entity.PenaltyTypeId = PenaltyRequestDTO.PenaltyTypeId;
                entity.PenaltyCaseId = PenaltyRequestDTO.PenaltyCaseId;
                entity.Code = PenaltyRequestDTO.Code;
                entity.CancelationReason = PenaltyRequestDTO.CancelationReason;
                entity.CancelationDecisionNo = PenaltyRequestDTO.CancelationDecisionNo;
                entity.CancelationDate = PenaltyRequestDTO.CancelationDate;
                entity.IsCanceled = PenaltyRequestDTO.IsCanceled;
                entity.DecisionDate = PenaltyRequestDTO.DecisionDate;
                entity.DecisionNo = PenaltyRequestDTO.DecisionNo;
                entity.EndDate = PenaltyRequestDTO.EndDate;
                entity.StartDate = PenaltyRequestDTO.StartDate;
                entity.EmpId = PenaltyRequestDTO.EmpId;

                await _unitOfWork.SaveAsync();

                var includes = new string[] { "Employee", "PenaltyType", "PenaltyCase" };
                var updatedPenalty = (await _unitOfWork.Penalties
                                              .FindAsync(p => p.Id == id, includes))
                                              .FirstOrDefault();

                if (updatedPenalty == null)
                    return StatusCode(500, "an error occured while updating");


                var GetDTO = profile.Penalty_To_GetPenaltyDTO(updatedPenalty);
                
                return Ok(GetDTO);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while creating: {e.Message}");
            }



        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Invalid Id");

            try
            {
                var entity = await _unitOfWork.Penalties.GetByIdAsync(id);

                if (entity == null)
                    return NotFound($"Penalty with Id {id} not found.");

                _unitOfWork.Penalties.Delete(entity.Id);
                await _unitOfWork.SaveAsync();

                return Ok("Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while creating: {e.Message}");

            }
        }


    }
}
