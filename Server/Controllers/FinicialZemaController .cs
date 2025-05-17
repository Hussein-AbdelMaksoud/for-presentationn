using Microsoft.AspNetCore.Mvc;
using Server.Mapping.Profile;
using Server.Models.DTOs.FinicialZema;
using Server.Services.UnitOfWork.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class FinicialZemaController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly FinicialZemaProfile _mapper;

    public FinicialZemaController(IUnitOfWork unitOfWork, FinicialZemaProfile mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetFinicialZemas()
    {
        var includes = new string[] { "Employee", "FinicialZemaType" };  // Changed from AllowanceType to FinicialZemaType
        var finicialZemas = await _unitOfWork.FinicialZemas.FindAsync(x => true, includes);

        if (!finicialZemas.Any())
            return NotFound("No FinicialZema Data found.");

        var output = _mapper.ToDtos(finicialZemas);
        return Ok(output);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFinicialZema(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid ID. Please provide a valid positive ID.");

        var includes = new string[] { "Employee", "FinicialZemaType" };  // Changed from AllowanceType to FinicialZemaType
        var finicialZema = (await _unitOfWork.FinicialZemas.FindAsync(x => x.Id == id, includes)).FirstOrDefault();

        if (finicialZema == null)
            return NotFound($"FinicialZema with ID {id} not found.");

        return Ok(_mapper.ToDto(finicialZema));
    }

    [HttpGet("Employee/{empId}")]
    public async Task<IActionResult> GetFinicialZemasByEmployee(string empId)
    {
        if (string.IsNullOrWhiteSpace(empId))
            return BadRequest("Invalid EmpId. Please provide a valid EmpId.");

        var employeeExists = await _unitOfWork.Employees.FindAsync(e => e.NationalId == empId);
        if (!employeeExists.Any())
            return NotFound("Employee not found. Please provide a valid EmpId.");

        var includes = new string[] { "Employee", "FinicialZemaType" };  // Changed from AllowanceType to FinicialZemaType
        var finicialZemas = await _unitOfWork.FinicialZemas.FindAsync(x => x.EmpId == empId, includes);

        if (!finicialZemas.Any())
            return NotFound($"No FinicialZema Data found for employee {empId}.");

        return Ok(_mapper.ToDtos(finicialZemas));
    }

    [HttpPost]
    public async Task<IActionResult> CreateFinicialZema([FromBody] CreateFinicialZemaDTO createDto)
    {
        if (createDto == null)
            return BadRequest("FinicialZema Data is required.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            // Validate Employee existence
            var employeeExists = await _unitOfWork.Employees
                .FindAsync(e => e.NationalId == createDto.EmpId);

            if (!employeeExists.Any())
                return NotFound("Employee not found. Please provide a valid EmpId.");

            // Validate FinicialZemaType existence if provided
            if (createDto.FinicialZemaTypeId.HasValue)
            {
                var finicialZemaType = await _unitOfWork.FinicialZemaTypes
                    .GetByIdAsync(createDto.FinicialZemaTypeId.Value);

                if (finicialZemaType == null)
                    return NotFound("FinicialZema Type not found. Please provide a valid FinicialZemaTypeId.");
            }

            // Check for duplicate Code
            var existingCode = await _unitOfWork.FinicialZemas
                .FindAsync(x => x.Code == createDto.Code);
            if (existingCode.Any())
                return Conflict($"A FinicialZema with the Code '{createDto.Code}' already exists.");

            var finicialZema = _mapper.ToEntity(createDto);
            await _unitOfWork.FinicialZemas.AddAsync(finicialZema);
            await _unitOfWork.SaveAsync();

            var relatedEntities = new[] { "Employee", "FinicialZemaType" };  // Changed from AllowanceType to FinicialZemaType
            var finicialZemaWithRelations = (await _unitOfWork.FinicialZemas
                .FindAsync(fz => fz.Id == finicialZema.Id, relatedEntities))
                .FirstOrDefault();

            if (finicialZemaWithRelations == null)
                return StatusCode(500, "FinicialZema Data retrieval after save returned null.");

            var finicialZemaDto = _mapper.ToDto(finicialZemaWithRelations);
            return Ok(finicialZemaDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the FinicialZema Data: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFinicialZema(int id, [FromBody] CreateFinicialZemaDTO updateDto)
    {
        if (updateDto == null)
            return BadRequest("FinicialZema data is required.");

        if (id <= 0)
            return BadRequest("Invalid ID. ID must be a positive number.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            // Retrieve existing finicialZema
            var existingFinicialZema = (await _unitOfWork.FinicialZemas.FindAsync(fz => fz.Id == id)).FirstOrDefault();
            if (existingFinicialZema == null)
                return NotFound($"FinicialZema with ID {id} not found.");

            // Validate Employee existence
            var employeeExists = await _unitOfWork.Employees.FindAsync(e => e.NationalId == updateDto.EmpId);
            if (!employeeExists.Any())
                return NotFound("Employee not found. Please provide a valid EmpId.");

            // Validate FinicialZemaType existence if provided
            if (updateDto.FinicialZemaTypeId.HasValue)
            {
                var finicialZemaTypeExists = await _unitOfWork.FinicialZemaTypes.GetByIdAsync(updateDto.FinicialZemaTypeId.Value);
                if (finicialZemaTypeExists == null)
                    return NotFound("FinicialZemaType not found. Please provide a valid FinicialZemaTypeId.");
            }

            // Check for duplicate Code (excluding current record)
            var existingCode = await _unitOfWork.FinicialZemas
                .FindAsync(x => x.Code == updateDto.Code && x.Id != id);
            if (existingCode.Any())
                return Conflict($"Another FinicialZema with the Code '{updateDto.Code}' already exists.");

            // Update the fields
            existingFinicialZema.Code = updateDto.Code;
            existingFinicialZema.EmpId = updateDto.EmpId;
            existingFinicialZema.FinicialZemaTypeId = updateDto.FinicialZemaTypeId;
            existingFinicialZema.GraftComingDate = updateDto.GraftComingDate;
            existingFinicialZema.GraftGoingDate = updateDto.GraftGoingDate;
            existingFinicialZema.LastDecisionDate = updateDto.LastDecisionDate;
            existingFinicialZema.SubmissionDate = updateDto.SubmissionDate;
            existingFinicialZema.NewSubmissionDate = updateDto.NewSubmissionDate;
            existingFinicialZema.Submitted = updateDto.Submitted;
            existingFinicialZema.Notes = updateDto.Notes;
            existingFinicialZema.RequiredZemaNo = updateDto.RequiredZemaNo;

            await _unitOfWork.SaveAsync();

            // Reload the entity with includes to return updated related data
            var includes = new string[] { "Employee", "FinicialZemaType" };  // Changed from AllowanceType to FinicialZemaType
            var updatedFinicialZema = (await _unitOfWork.FinicialZemas.FindAsync(fz => fz.Id == id, includes)).FirstOrDefault();

            if (updatedFinicialZema == null)
                return StatusCode(500, "An error occurred while retrieving the updated FinicialZema.");

            return Ok(_mapper.ToDto(updatedFinicialZema));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the FinicialZema: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFinicialZema(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid ID. ID must be a positive number.");

        try
        {
            var finicialZema = await _unitOfWork.FinicialZemas.GetByIdAsync(id);
            if (finicialZema == null)
                return NotFound($"FinicialZema with ID {id} not found.");

            _unitOfWork.FinicialZemas.Delete(finicialZema.Id);
            await _unitOfWork.SaveAsync();

            return Ok("FinicialZema deleted successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the FinicialZema: {ex.Message}");
        }
    }
}