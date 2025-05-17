using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.Allowance;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class AllowanceController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AllowanceProfile _mapper;

    public AllowanceController(IUnitOfWork unitOfWork, AllowanceProfile mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllowances()
    {
        var includes = new string[] { "Employee", "AllowanceType" };
        var allowances = await _unitOfWork.Allowances.FindAsync(x => true, includes);
        if (!allowances.Any())
            return NotFound("No Allowance Data found.");

        var output = _mapper.ToDtos(allowances);
        return Ok(output);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllowance(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid ID. Please provide a valid positive ID.");

        var includes = new string[] { "Employee", "AllowanceType" };
        var allowance = (await _unitOfWork.Allowances.FindAsync(x => x.Id == id, includes)).FirstOrDefault();

        if (allowance == null)
            return NotFound($"Allowance with ID {id} not found.");

        return Ok(_mapper.ToDto(allowance));
    }

    [HttpGet("Employee/{empId}")]
    public async Task<IActionResult> GetAllowancesByEmployee(string empId)
    {
        if (string.IsNullOrWhiteSpace(empId))
            return BadRequest("Invalid EmpId. Please provide a valid EmpId.");

        // Check if employee exists
        var employeeExists = await _unitOfWork.Employees.FindAsync(e => e.NationalId == empId);
        if (!employeeExists.Any())
            return NotFound("Employee not found. Please provide a valid EmpId.");

        var includes = new string[] { "Employee", "AllowanceType" };
        var allowances = await _unitOfWork.Allowances.FindAsync(x => x.EmpId == empId, includes);

        if (!allowances.Any())
            return NotFound($"No Allowance Data found for employee {empId}.");

        return Ok(_mapper.ToDtos(allowances));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAllowanceDTO createDto)
    {
        if (createDto == null)
            return BadRequest("Allowance Data is required.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var employeeExists = await _unitOfWork.Employees
                .FindAsync(e => e.NationalId == createDto.EmpId);

            if (!employeeExists.Any())
                return NotFound("Employee not found. Please provide a valid EmpId.");

            if (createDto.AllowanceTypeId.HasValue)
            {
                var allowanceType = await _unitOfWork.AllowanceTypes
                    .GetByIdAsync(createDto.AllowanceTypeId.Value);

                if (allowanceType == null)
                    return NotFound("Allowance Type not found. Please provide a valid AllowanceTypeId.");
            }



            var allowance = _mapper.ToEntity(createDto);

            await _unitOfWork.Allowances.AddAsync(allowance);
            await _unitOfWork.SaveAsync();

            var relatedEntities = new[] { "Employee", "AllowanceType" };
            var AllowanceDataWithRelations = (await _unitOfWork.Allowances
                .FindAsync(md => md.Id == allowance.Id, relatedEntities))
                .FirstOrDefault();

            if (AllowanceDataWithRelations == null)
                return StatusCode(500, "Allowance Data retrieval after save returned null.");

            var mandateDataDto = _mapper.ToDto(AllowanceDataWithRelations);

            return Ok(mandateDataDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the Allowance Data: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAllowance(int id, [FromBody] CreateAllowanceDTO updateDto)
    {
        if (updateDto == null)
            return BadRequest("Allowance data is required.");

        if (id <= 0)
            return BadRequest("Invalid ID. ID must be a positive number.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            // Retrieve existing allowance (without includes for update).
            var existingAllowance = (await _unitOfWork.Allowances.FindAsync(a => a.Id == id)).FirstOrDefault();
            if (existingAllowance == null)
                return NotFound($"Allowance with ID {id} not found.");

            // Validate Employee existence.
            var employeeExists = await _unitOfWork.Employees.FindAsync(e => e.NationalId == updateDto.EmpId);
            if (!employeeExists.Any())
                return NotFound("Employee not found. Please provide a valid EmpId.");

            // Validate AllowanceType existence if provided.
            if (updateDto.AllowanceTypeId.HasValue)
            {
                var allowanceTypeExists = await _unitOfWork.AllowanceTypes.GetByIdAsync(updateDto.AllowanceTypeId.Value);
                if (allowanceTypeExists == null)
                    return NotFound("AllowanceType not found. Please provide a valid AllowanceTypeId.");
            }

            // Update the fields.
            existingAllowance.DecisionNo = updateDto.DecisionNo;
            existingAllowance.DecisionDate = updateDto.DecisionDate;
            existingAllowance.Code = updateDto.Code;
            existingAllowance.EmpId = updateDto.EmpId;
            existingAllowance.AllowanceTypeId = updateDto.AllowanceTypeId;

            await _unitOfWork.SaveAsync();

            // Reload the entity with includes to return updated related data.
            var includes = new string[] { "Employee", "AllowanceType" };
            var updatedAllowance = (await _unitOfWork.Allowances.FindAsync(a => a.Id == id, includes)).FirstOrDefault();

            if (updatedAllowance == null)
                return StatusCode(500, "An error occurred while retrieving the updated allowance.");

            return Ok(_mapper.ToDto(updatedAllowance));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the allowance: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAllowance(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid ID. ID must be a positive number.");

        try
        {
            var allowance = await _unitOfWork.Allowances.GetByIdAsync(id);
            if (allowance == null)
                return NotFound($"Allowance with ID {id} not found.");

            _unitOfWork.Allowances.Delete(allowance.Id);
            await _unitOfWork.SaveAsync();

            return Ok("Allowance deleted successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the allowance: {ex.Message}");
        }
    }
}
