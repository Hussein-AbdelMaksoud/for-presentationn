using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.YearReportLaw_;
using Server.Services.UnitOfWork.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class YearReportLawController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly YearReportLawProfile _mapper;

    public YearReportLawController(IUnitOfWork unitOfWork, YearReportLawProfile mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> GetYearReportLaws()
    {
        var includes = new string[] { "Employee", };

        // Retrieve all YearReportLaw records.
        var yearReportLaws = await _unitOfWork.YearReportLaws.FindAsync(x => true, includes);

        if (!yearReportLaws.Any())
        {
            return NotFound("No YearReportLaws found.");
        }

        var output = _mapper.ToDtos(yearReportLaws);
        return Ok(output);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetYearReportLaw(int id)
    {
        var includes = new string[] { "Employee", };

        // Retrieve a specific YearReportLaw by id.
        var YearReportLaw = (await _unitOfWork.YearReportLaws.FindAsync(x => x.Id == id, includes)).FirstOrDefault();

        if (YearReportLaw == null)
        {
            return NotFound();
        }

        return Ok(_mapper.ToDto(YearReportLaw));
    }



    [HttpGet("Employee/{empId}")]
    public async Task<IActionResult> GetYearReportLawsByEmployee(string empId)
    {


        var employeeExists = await _unitOfWork.Employees
            .FindAsync(e => e.NationalId == empId);

        if (!employeeExists.Any())
            return NotFound("Employee not found. Please provide a valid EmpId.");


        var includes = new string[] { "Employee" };
        var YearReportLaws = await _unitOfWork.YearReportLaws.FindAsync(x => x.EmpId == empId, includes);



        return Ok(_mapper.ToDtos(YearReportLaws));
    }








    [HttpPost]
    public async Task<IActionResult> CreateYearReportLaw(CreateYearReportLawDTO createDto)
    {
        if (createDto == null)
        {
            return BadRequest("YearReportLaw data is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Check for duplicate entry based on a unique property (e.g., Code)
            var existing = (await _unitOfWork.YearReportLaws.FindAsync(x => x.Code == createDto.Code)).FirstOrDefault();
            if (existing != null)
            {
                return Conflict($"A YearReportLaw with the Code '{createDto.Code}' already exists.");
            }

            // Map the DTO to the entity and save.
            var entity = _mapper.ToEntity(createDto);
            await _unitOfWork.YearReportLaws.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            return Ok(createDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the YearReportLaw: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateYearReportLaw(int id, CreateYearReportLawDTO updateDto)
    {
        if (updateDto == null)
            return BadRequest("YearReportLaw data is required.");

        if (id <= 0)
            return BadRequest("Invalid id. id must be a positive number.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var includes = new string[] { "Employee", };

        // Retrieve the existing YearReportLaw.
        var existingYearReportLaw = (await _unitOfWork.YearReportLaws.FindAsync(x => x.Id == id, includes)).FirstOrDefault();
        if (existingYearReportLaw == null)
        {
            return NotFound();
        }

        // Update properties.

        existingYearReportLaw.Code = updateDto.Code;
        existingYearReportLaw.Period = updateDto.Period;
        existingYearReportLaw.Geha = updateDto.Geha;
        existingYearReportLaw.Grade = updateDto.Grade;
        existingYearReportLaw.EmpId = updateDto.EmpId;
        await _unitOfWork.SaveAsync();

        // Optionally reload the entity (if needed for related data)
        var updatedYearReportLaw = (await _unitOfWork.YearReportLaws.FindAsync(x => x.Id == id)).FirstOrDefault();
        return Ok(_mapper.ToDto(updatedYearReportLaw));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteYearReportLaw(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid ID.");

        try
        {
            var yearReportLaw = await _unitOfWork.YearReportLaws.GetByIdAsync(id);
            if (yearReportLaw == null)
                return NotFound($"YearReportLaw with ID {id} not found.");

            _unitOfWork.YearReportLaws.Delete(yearReportLaw.Id);
            await _unitOfWork.SaveAsync();

            return Ok("YearReportLaw deleted successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the YearReportLaw: {ex.Message}");
        }
    }
}
