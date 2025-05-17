using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.AllowanceType;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowanceTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AllowanceTypeProfile _mapper;

        public AllowanceTypeController(IUnitOfWork unitOfWork, AllowanceTypeProfile mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all allowance types.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allowanceTypes = await _unitOfWork.AllowanceTypes.GetAllAsync();
                if (allowanceTypes == null || !allowanceTypes.Any())
                {
                    return NotFound("No allowance types found.");
                }

                // Use Mapperly to convert the entity list to a DTO list.
                var dtos = _mapper.ToDtos(allowanceTypes);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                // In production, consider logging the exception details.
                return StatusCode(500, $"An error occurred while retrieving allowance types: {ex.Message}");
            }
        }


        /// <summary>
        /// Retrieves a specific allowance type by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            try
            {
                var allowanceType = await _unitOfWork.AllowanceTypes.GetByIdAsync(id);
                if (allowanceType == null)
                    return NotFound($"Allowance type with ID {id} not found.");

                var dto = _mapper.ToDto(allowanceType);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the allowance type: {ex.Message}");
            }
        }




        /// <summary>
        /// Retrieves a specific allowance type by its Name.
        /// </summary>
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Invalid name.");

            try
            {
                // Retrieve the allowance type by its name
                var allowanceType = await _unitOfWork.AllowanceTypes.FindAsync(a => a.Name.Contains(name));
                if (allowanceType == null || !allowanceType.Any())
                    return NotFound($"No allowanceType found with name '{name}'.");

                var output = _mapper.ToDtos(allowanceType);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the allowance type: {ex.Message}");
            }
        }




        /// <summary>
        /// Creates a new allowance type.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAllowanceTypeDTO allowanceType)
        {
            if (allowanceType == null)
                return BadRequest("Allowance type data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Check if an allowance type with the same name already exists.
                var existing = await _unitOfWork.AllowanceTypes
                    .GetByNameAsync(a => a.Name.ToLower() == allowanceType.Name.ToLower());

                if (existing != null)
                {
                    return Conflict($"An allowance type with the name '{allowanceType.Name}' already exists.");
                }

                // Map the DTO to the entity.
                var allowanceTypeEntity = _mapper.ToEntity(allowanceType);
                await _unitOfWork.AllowanceTypes.AddAsync(allowanceTypeEntity);
                await _unitOfWork.SaveAsync();


                return Ok(allowanceType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the allowance type: {ex.Message}");
            }
        }





        /// <summary>
        /// Updates an existing allowance type.
        /// </summary>

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateAllowanceTypeDTO CreateAllowanceTypeDTO)
        {
            if (CreateAllowanceTypeDTO == null)
                return BadRequest("Allowance Type data is required.");

            if (id <= 0)
                return BadRequest("Invalid id. id must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existinAllowanceType = await _unitOfWork.AllowanceTypes.GetByIdAsync(id);

                if (existinAllowanceType == null)
                    return NotFound($"Allowance Type with id {id} not found.");

                // Check if another sector with the same name exists
                var duplicateAllowanceType = await _unitOfWork.AllowanceTypes
                    .FindAsync(s => s.Name.ToLower() == CreateAllowanceTypeDTO.Name.ToLower() && s.Id != id);

                if (duplicateAllowanceType.Any())
                    return Conflict($"A Allowance Type with the name '{CreateAllowanceTypeDTO.Name}' already exists.");

                existinAllowanceType.Name = CreateAllowanceTypeDTO.Name;
                existinAllowanceType.Code = CreateAllowanceTypeDTO.Code;
                await _unitOfWork.SaveAsync();

                return Ok(CreateAllowanceTypeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Allowance Type: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an existing allowance type.
        /// </summary>

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            try
            {
                var allowanceType = await _unitOfWork.AllowanceTypes.GetByIdAsync(id);
                if (allowanceType == null)
                    return NotFound($"Allowance type with ID {id} not found.");

                // Optional: Check for related dependencies before deletion.

                _unitOfWork.AllowanceTypes.Delete(allowanceType.Id);
                await _unitOfWork.SaveAsync();

                // Return NoContent to indicate successful deletion.
                return Ok("Allowance type deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the allowance type: {ex.Message}");
            }
        }
    }
}
