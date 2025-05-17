using Microsoft.AspNetCore.Mvc;
using Server.Mapping.Profile;
using Server.Models.DTOs.Lagna;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LagnaController : ControllerBase
{

    string[] relatedEntities = new string[] { "Employee" };

    private readonly IUnitOfWork _unitOfWork;
    private readonly LagnaProfile _profile;

    public LagnaController(IUnitOfWork unitOfWork, LagnaProfile profile)
    {
        _unitOfWork = unitOfWork;
        _profile = profile;
    }


    /// <summary>
    /// GET: api/Lagna
    /// - **Description**: Retrieves all Lagna records with related Employee data from the database.
    /// - **URL**: `GET api/Lagna`
    /// - **Response**: A list of all Lagna records.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetLagna()
    {
        try
        {
            var lagnaRecords = await _unitOfWork.Lagnas.FindAsync(x => true, relatedEntities);

            var output = _profile.ToDTOs(lagnaRecords);
            return Ok(output);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving Lagna: {ex.Message}");
        }
    }


    /// <summary>
    /// GET: api/Lagna/{id}
    /// - **Description**: Retrieves a Lagna record by its ID with related Employee data.
    /// - **URL**: `GET api/Lagna/{id}`
    /// - **Response**: The Lagna record with the given ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLagnaById(int id)
    {
        try
        {
            var lagnaRecord = (await _unitOfWork.Lagnas.FindAsync(l => l.Id == id, relatedEntities)).FirstOrDefault();

            if (lagnaRecord == null)
            {
                return NotFound();
            }

            return Ok(_profile.ToDTO(lagnaRecord));
        } 
        
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving Lagna: {ex.Message}");
        }
    }


    /// <summary>
    /// GET: api/Lagna/GetByEmpId/{empId}
    /// - **Description**: Retrieves Lagna records for a specific employee by their EmpId.
    /// - **URL**: `GET api/Lagna/GetByEmpId/{empId}`
    /// - **Response**: A list of Lagna records for the employee with the given EmpId.
    /// </summary>

    [HttpGet("Employee/{empId}")]
    public async Task<IActionResult> GetByEmpId(string empId)
    {
        if (string.IsNullOrWhiteSpace(empId))
            return BadRequest("Invalid EmpId. Please provide a valid EmpId.");

        empId = empId.Trim();

        try
        {

            var lagnas = await _unitOfWork.Lagnas
                .FindAsync(l => l.EmpId == empId, relatedEntities);

            if (!lagnas.Any())
                return NotFound($"No Lagna found for EmpId '{empId}'.");

            var lagnaDtos = _profile.ToDTOs(lagnas);
            return Ok(lagnaDtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving Lagna for EmpId '{empId}': {ex.Message}");
        }
    }

    /// <summary>
    /// POST: api/Lagna
    /// - **Description**: Creates a new Lagna record after checking for duplicate and employee existence.
    /// - **URL**: `POST api/Lagna`
    /// - **Response**: The created Lagna record.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateLagna(CreateLagnaDTO createDto)
    {
        if (createDto == null)
        {
            return BadRequest("Lagna data is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var employee = (await _unitOfWork.Employees.FindAsync(e => e.NationalId == createDto.EmpId)).FirstOrDefault();
            if (employee == null)
            {
                return NotFound($"Employee with ID '{createDto.EmpId}' not found.");
            }

            // التحقق من عدم تكرار اللجنة لنفس الموظف بنفس الدور
            // var existingLagna = (await _unitOfWork.Lagnas
            //     .FindAsync(l => l.EmpId == createDto.EmpId && l.Name == createDto.Name && l.MemberType == createDto.MemberType))
            //     .FirstOrDefault();
            // if (existingLagna != null)
            // {
            //     return Conflict("Employee is already assigned to this committee with the same role.");
            // }

            var entity = _profile.ToCreateEntity(createDto);
            await _unitOfWork.Lagnas.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            var addedLagna = (await _unitOfWork.Lagnas.FindAsync(x => x.Id == entity.Id)).FirstOrDefault();
            if (addedLagna == null)
            {
                return StatusCode(500, "Failed to retrieve created Lagna.");
            }

            var output = _profile.ToDTO(addedLagna);
            return Ok(output);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating Lagna: {ex.Message}");
        }
    }


    /// <summary>
    /// PUT: api/Lagna/{id}
    /// - **Description**: Updates an existing Lagna record by its ID after validation.
    /// - **URL**: `PUT api/Lagna/{id}`
    /// - **Response**: The updated Lagna record.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLagna(int id, CreateLagnaDTO updateDto)
    {
        if (updateDto == null)
        {
            return BadRequest("Lagna data is required.");
        }

        if (id <= 0)
        {
            return BadRequest("Invalid id. id must be a positive number.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var existingLagna = (await _unitOfWork.Lagnas.FindAsync(l => l.Id == id)).FirstOrDefault();
            if (existingLagna == null)
            {
                return NotFound($"Lagna with ID '{id}' not found.");
            }

            var employee = (await _unitOfWork.Employees.FindAsync(e => e.NationalId == updateDto.EmpId)).FirstOrDefault();
            if (employee == null)
            {
                return NotFound($"Employee with ID '{updateDto.EmpId}' not found.");
            }

            // التحقق من عدم تكرار نفس البيانات للموظف باستثناء السجل الحالي
            // var duplicateLagna = (await _unitOfWork.Lagnas
            //     .FindAsync(l => l.EmpId == updateDto.EmpId && l.Name == updateDto.Name && l.MemberType == updateDto.MemberType && l.Id != id))
            //     .FirstOrDefault();
            // if (duplicateLagna != null)
            // {
            //     return Conflict("Employee is already a member of this committee with the same role.");
            // }

            existingLagna.Code = updateDto.Code;
            existingLagna.Name = updateDto.Name;
            existingLagna.EmpId = updateDto.EmpId;
            existingLagna.MemberType = updateDto.MemberType;
            existingLagna.DecisionNo = updateDto.DecisionNo;
            existingLagna.DecisionDate = updateDto.DecisionDate;

            await _unitOfWork.SaveAsync();

            var updatedLagnaDto = _profile.ToDTO(existingLagna);
            return Ok(updatedLagnaDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating Lagna: {ex.Message}");
        }
    }



    /// <summary>
    /// DELETE: api/Lagna/{id}
    /// - **Description**: Deletes a Lagna record by its ID.
    /// - **URL**: `DELETE api/Lagna/{id}`
    /// - **Response**: A success message indicating the deletion of the Lagna record.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLagna(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid ID.");

        try
        {
            var lagnaRecord = await _unitOfWork.Lagnas.GetByIdAsync(id);
            if (lagnaRecord == null)
                return NotFound($"Lagna with ID {id} not found.");

            _unitOfWork.Lagnas.Delete(lagnaRecord.Id);
            await _unitOfWork.SaveAsync();

            return Ok("Lagna deleted successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the Lagna: {ex.Message}");
        }
    }
}