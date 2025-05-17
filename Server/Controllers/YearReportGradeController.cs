using Microsoft.AspNetCore.Mvc;
using Server.Mapping.Profile;
using Server.Models.DTOs.YearReportGrade;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class YearReportGradeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly YearReportGradeProfile profile;
     
    public YearReportGradeController(IUnitOfWork unitOfWork, YearReportGradeProfile profile)
    {
        _unitOfWork = unitOfWork;
        this.profile = profile;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var entitys = await _unitOfWork.YearReportGrades.GetAllAsync();

            if (entitys == null || !entitys.Any())
            {
                return NotFound("No Year Report Grades  Found.!");

            }

            var getDTO = profile.YearReportGradeList_To_GetYearReportGradeDTOList(entitys);
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
            var entity = await _unitOfWork.YearReportGrades.GetByIdAsync(id);

            if (entity == null)
                return NotFound($"!Year Report Grade With Id{id} NOT FOUND.");

            var GetDTO = profile.YearReportGrade_To_GetYearReportGradeDTO(entity);
            return Ok(GetDTO);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An Error Occured {e.Message}");
        }
    }


    [HttpGet("byname/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Invalid name.");

        try
        {


            var entities = await _unitOfWork.YearReportGrades.FindAsync(e => e.Name.ToLower().Contains(name.ToLower()));

            if (entities == null || !entities.Any())
                return NotFound($"No Year Report Grades found With Name{name}");

            var GetDTO = profile.YearReportGradeList_To_GetYearReportGradeDTOList(entities);
            return Ok(GetDTO);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An Error Occured {e.Message}");
        }
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] YearReportGradeDTO YearReportGradeRequest)
    {
        if (YearReportGradeRequest == null)
            return BadRequest("Year Report  Grade data is Required.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var IsExist = await _unitOfWork.YearReportGrades
                                 .GetByNameAsync(a => a.Name.ToLower() == YearReportGradeRequest.Name.ToLower());

            if (IsExist != null)
                return Conflict($"A Year Report Grade  With The Name{YearReportGradeRequest.Name} already exists.");


            var entity = profile.YearReportGradeDTO_To_YearReportGrade(YearReportGradeRequest);

            await _unitOfWork.YearReportGrades.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            return Ok(YearReportGradeRequest);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating: {e.Message}");
        }

    }



    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] YearReportGradeDTO YearReportGradeRequest)
    {
        if (YearReportGradeRequest == null)
            return BadRequest("Year Report Grade  data is Required.");

        if (id <= 0)
            return BadRequest("!Invalid Id.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var entity = await _unitOfWork.YearReportGrades.GetByIdAsync(id);

            if (entity == null)
                return BadRequest($"Year Report Grade With id {id} not found. ");

            var duplicateEntity = await _unitOfWork
                                                 .YearReportGrades
                                                 .FindAsync(e => e.Name.ToLower() == YearReportGradeRequest.Name.ToLower() &&
                                                 e.Code == YearReportGradeRequest.Code
                                                 );

            if (duplicateEntity.Any())
                return Conflict($"Year Report Grade  With The Same Code {YearReportGradeRequest.Code} and Name {YearReportGradeRequest.Name} already Exist");

            entity.Name = YearReportGradeRequest.Name;
            entity.Code = YearReportGradeRequest.Code;

            await _unitOfWork.SaveAsync();

            return Ok(YearReportGradeRequest);

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
            var entity = await _unitOfWork.YearReportGrades.GetByIdAsync(id);

            if (entity == null)
                return NotFound($"Year Report Grade with Id {id} not found.");

            _unitOfWork.YearReportGrades.Delete(entity.Id);
            await _unitOfWork.SaveAsync();

            return Ok("Deleted");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating: {e.Message}");

        }
    }


}
