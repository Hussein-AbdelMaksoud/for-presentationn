using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Mapping.Profile;
using Server.Models.DTOs.YearReport;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YearReportController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly YearReportProfile profile;

        public YearReportController(IUnitOfWork unitOfWork, YearReportProfile profile)
        {
            _unitOfWork = unitOfWork;
            this.profile = profile;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var includes = new string[] { "Employee", "YearReportGrade" };

                var entitys = await _unitOfWork.YearReports.FindAsync(x=> true , includes);

                if (entitys == null || !entitys.Any())
                {
                    return NotFound("No YearReports  Found.!");

                }

                var getDTO = profile.YearReportList_To_GetYearReportDTOList(entitys);
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
                var includes = new string[] { "Employee", "YearReportGrade" };

                var entity = (await _unitOfWork.YearReports.FindAsync(x=> x.Id == id , includes)).FirstOrDefault();

                if (entity == null)
                    return NotFound($"!YearReport With Id{id} NOT FOUND.");

                var GetDTO = profile.YearReport_To_GetYearReportDTO(entity);
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
        public async Task<IActionResult> GetYearReportByEmployee(string empId)
        {
            if (string.IsNullOrWhiteSpace(empId))
                return BadRequest("Invalid EmpId. Please provide a valid EmpId.");

            // Check if employee exists
            var employeeExists = await _unitOfWork.Employees.FindAsync(e => e.NationalId == empId);
            if (!employeeExists.Any())
                return NotFound("Employee not found. Please provide a valid EmpId.");

            var includes = new string[] { "Employee", "YearReportGrade" };

            var entities = await _unitOfWork.YearReports.FindAsync(x => x.EmpId == empId, includes);

            if (!entities.Any())
                return NotFound($"No Allowance Data found for employee {empId}.");

            return Ok(profile.YearReportList_To_GetYearReportDTOList(entities));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateYearReportDTO YearReportRequestDTO)
        {
            if (YearReportRequestDTO == null)
                return BadRequest("YearReport data is Required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var employeeExists = await _unitOfWork.Employees
                                            .FindAsync(e => e.NationalId == YearReportRequestDTO.EmpId);

                if (!employeeExists.Any())
                    return NotFound("Employee Not Found . please Enter a valid EmpId.");

                if (YearReportRequestDTO.GradeId.HasValue)
                {
                    var YearReportGrade = await _unitOfWork.YearReportGrades.GetByIdAsync(YearReportRequestDTO.GradeId.Value);
                    if (YearReportGrade == null)
                        return NotFound("Year report grade not found");
                }

                var entity = profile.YearReportDTO_To_YearReport(YearReportRequestDTO);

                await _unitOfWork.YearReports.AddAsync(entity);
                await _unitOfWork.SaveAsync();

                return Ok(YearReportRequestDTO);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while creating: {e.Message}");
            }

        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateYearReportDTO YearReportRequestDTO)
        {
            if (YearReportRequestDTO == null)
                return BadRequest("YearReport data is Required.");

            if (id <= 0)
                return BadRequest("!Invalid Id.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var entity = await _unitOfWork.YearReports.GetByIdAsync(id);

                if (entity == null)
                    return BadRequest($"YearReport With id {id} not found. ");

                var employeeExist = await _unitOfWork.Employees.FindAsync(e => e.NationalId == YearReportRequestDTO.EmpId);
                if (employeeExist == null)
                    return NotFound("Employee not Found");

                var YearReportGradeExist = await _unitOfWork.YearReportGrades.GetByIdAsync(int.Parse( YearReportRequestDTO.GradeId.ToString()));
                if (YearReportGradeExist == null)
                    return BadRequest("YearReportGrade Not Found");

                entity.Year = YearReportRequestDTO.Year;
                entity.Degree = YearReportRequestDTO.Degree;
                entity.Code = YearReportRequestDTO.Code;
                entity.Notes = YearReportRequestDTO.Notes;
                entity.GradeId = YearReportRequestDTO.GradeId;
                entity.EmpId = YearReportRequestDTO.EmpId;
                entity.Id = id;

                await _unitOfWork.SaveAsync();

                var includes = new string[] { "Employee", "YearReportGrade" };
                var updatedYearReport = (await _unitOfWork.YearReports.FindAsync(y => y.Id == id, includes)).FirstOrDefault();


                return Ok(profile.YearReport_To_GetYearReportDTO(updatedYearReport));

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
                var entity = await _unitOfWork.YearReports.GetByIdAsync(id);

                if (entity == null)
                    return NotFound($"YearReports with Id {id} not found.");

                _unitOfWork.YearReports.Delete(entity.Id);
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
