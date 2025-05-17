using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.MilitaryState;
using Server.Services.UnitOfWork.Interfaces;
using Server.Mapping.Profile;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilitaryStateController : ControllerBase
    {

        string[] relatedEntities = new string[] { "Employee", "MilitaryStateType" };

        private readonly IUnitOfWork _unitOfWork;
        private readonly MilitaryStateProfile _profile;

        public MilitaryStateController(IUnitOfWork unitOfWork, MilitaryStateProfile profile)
        {
            _unitOfWork = unitOfWork;
            _profile = profile;
        }

        #region Endpoints


        /// <summary>
        /// Get all Military States.
        /// URL: GET api/MilitaryState
        /// </summary>
        /// <returns>List of MilitaryStateDTOs</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                var militaryStates = await _unitOfWork.MilitaryStates
                    .FindAsync(x => true, relatedEntities);

                if (!militaryStates.Any())
                    return NotFound("No Military States found.");

                var militaryStateDtos = _profile.ToDTOs(militaryStates);
                return Ok(militaryStateDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Military States: {ex.Message}");
            }
        }


        /// <summary>
        /// Get Military State by ID.
        /// URL: GET api/MilitaryState/{id}
        /// </summary>
        /// <param name="id">Military State ID</param>
        /// <returns>MilitaryStateDTO</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. Please provide a valid positive ID.");

            try
            {

                var militaryState = await _unitOfWork.MilitaryStates
                    .FindAsync(ms => ms.Id == id, relatedEntities);

                var result = militaryState.FirstOrDefault();

                if (result == null)
                    return NotFound($"Military State with ID {id} not found.");

                var militaryStateDto = _profile.ToDTO(result);
                return Ok(militaryStateDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the Military State: {ex.Message}");
            }
        }


        /// <summary>
        /// Get Military States by Employee ID.
        /// URL: GET api/MilitaryState/by-empid/{empId}
        /// </summary>
        /// <param name="empId">Employee National ID</param>
        /// <returns>List of MilitaryStateDTOs</returns>
        [HttpGet("Employee/{empId}")]
        public async Task<IActionResult> GetByEmpId(string empId)
        {
            if (string.IsNullOrWhiteSpace(empId))
                return BadRequest("EmpId cannot be empty.");

            empId = empId.Trim();

            try
            {

                var militaryStates = await _unitOfWork.MilitaryStates
                    .FindAsync(ms => ms.EmpId == empId, relatedEntities);

                if (!militaryStates.Any())
                    return NotFound($"No Military States found for EmpId '{empId}'.");

                var militaryStateDtos = _profile.ToDTOs(militaryStates);
                return Ok(militaryStateDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Military States: {ex.Message}");
            }
        }


        /// <summary>
        /// Create a new Military State.
        /// URL: POST api/MilitaryState
        /// </summary>
        /// <param name="createDto">CreateMilitaryStateDTO object</param>
        /// <returns>Created MilitaryStateDTO</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMilitaryStateDTO createDto)
        {
            if (createDto == null)
                return BadRequest("Military State data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (createDto.MilitaryStateTypeId == null || createDto.MilitaryStateTypeId == 0)
                return BadRequest("Military State Type is required.");

            try
            {
                var employeeExists = await _unitOfWork.Employees
                    .FindAsync(e => e.NationalId == createDto.EmpId);

                if (!employeeExists.Any())
                    return NotFound("Employee not found. Please provide a valid EmpId.");

                if (createDto.CurrentMilitaryState)
                {
                    var currentMilitaryState = await _unitOfWork.MilitaryStates
                        .FindAsync(ms => ms.EmpId == createDto.EmpId && ms.CurrentMilitaryState);

                    if (currentMilitaryState.Any())
                        return Conflict("An active Military State already exists for this employee.");
                }

                var militaryState = _profile.ToCreateEntity(createDto);

                await _unitOfWork.MilitaryStates.AddAsync(militaryState);
                await _unitOfWork.SaveAsync();

                var militaryStateWithRelations = (await _unitOfWork.MilitaryStates
                    .FindAsync(ms => ms.Id == militaryState.Id, relatedEntities))
                    .FirstOrDefault();

                if (militaryStateWithRelations == null)
                    return StatusCode(500, "Military State retrieval after save returned null.");

                var militaryStateDto = _profile.ToDTO(militaryStateWithRelations);

                return CreatedAtAction(nameof(GetById), new { id = militaryStateDto.Id }, militaryStateDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Military State: {ex.Message}");
            }
        }


        /// <summary>
        /// Update an existing Military State.
        /// URL: PUT api/MilitaryState/{id}
        /// </summary>
        /// <param name="id">Military State ID</param>
        /// <param name="updateDto">CreateMilitaryStateDTO object</param>
        /// <returns>Updated MilitaryStateDTO</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateMilitaryStateDTO updateDto)
        {
            if (updateDto == null)
                return BadRequest("Military State data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (updateDto.MilitaryStateTypeId == null || updateDto.MilitaryStateTypeId == 0)
                return BadRequest("Military State Type is required.");

            try
            {
                var existingMilitaryState = await _unitOfWork.MilitaryStates
                    .FindAsync(ms => ms.Id == id, relatedEntities);

                var militaryState = existingMilitaryState.FirstOrDefault();

                if (militaryState == null)
                    return NotFound($"Military State with ID {id} not found.");

                var employeeExists = await _unitOfWork.Employees
                    .FindAsync(e => e.NationalId == updateDto.EmpId);

                if (!employeeExists.Any())
                    return NotFound("Employee not found. Please provide a valid EmpId.");

                if (updateDto.CurrentMilitaryState && !militaryState.CurrentMilitaryState)
                {
                    var currentMilitaryState = await _unitOfWork.MilitaryStates
                        .FindAsync(ms => ms.EmpId == updateDto.EmpId && ms.CurrentMilitaryState && ms.Id != id);

                    if (currentMilitaryState.Any())
                        return Conflict("An active Military State already exists for this employee.");
                }

                militaryState.Code = updateDto.Code;
                militaryState.EmpId = updateDto.EmpId;
                militaryState.MilitaryStateTypeId = updateDto.MilitaryStateTypeId;
                militaryState.Date = updateDto.Date;
                militaryState.Notes = updateDto.Notes;
                militaryState.CurrentMilitaryState = updateDto.CurrentMilitaryState;

                _unitOfWork.MilitaryStates.Update(militaryState);
                await _unitOfWork.SaveAsync();

                var updatedMilitaryState = (await _unitOfWork.MilitaryStates
                    .FindAsync(ms => ms.Id == id, relatedEntities))
                    .FirstOrDefault();

                if (updatedMilitaryState == null)
                    return StatusCode(500, "Military State retrieval after update returned null.");

                var updatedDto = _profile.ToDTO(updatedMilitaryState);
                return Ok(updatedDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Military State: {ex.Message}");
            }
        }


        /// <summary>
        /// Delete a Military State by ID.
        /// URL: DELETE api/MilitaryState/{id}
        /// </summary>
        /// <param name="id">Military State ID</param>
        /// <returns>No content on success</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var militaryState = await _unitOfWork.MilitaryStates.GetByIdAsync(id);
                if (militaryState == null)
                    return NotFound($"Military State with ID {id} not found.");

                _unitOfWork.MilitaryStates.Delete(militaryState.Id);
                await _unitOfWork.SaveAsync();

                return Ok("Military State deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Military State: {ex.Message}");
            }
        }

        #endregion
    }
}