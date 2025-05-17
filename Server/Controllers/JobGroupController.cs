using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Entities;
using Server.Mapping.Profile;
using Server.Services.UnitOfWork.Interfaces;
using Server.Models.DTOs.JobGroup;
using Server.Models.DTOs.BaseDTOs;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobGroupController : ControllerBase
    {
        // Declare the UnitOfWork interface to be used for CRUD operations
        private readonly IUnitOfWork _unitOfWork;
        private readonly JobGroupProfile _mapper;

        // Initialize the controller with dependency injection for UnitOfWork
        public JobGroupController(IUnitOfWork unitOfWork, JobGroupProfile mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var jobGroups = await _unitOfWork.JobGroups.GetAllAsync();

                if (jobGroups == null || !jobGroups.Any())
                    return NotFound("No job groups found.");

                // Use Mapperly to convert the entity list to a DTO list.
                var dtos = _mapper.MapJobGroupListToBaseDTOList(jobGroups);
                return Ok(dtos);
            }
            catch
            {
                return StatusCode(500, "Error retrieving job groups.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id.");

            try
            {
                var jobGroup = await _unitOfWork.JobGroups.GetByIdAsync(id);

                if (jobGroup == null)
                    return NotFound($"Job group with ID {id} not found.");
                BaseDTO dto = _mapper.MapJobGroupToBaseDTO(jobGroup);
                return Ok(dto);
            }
            catch
            {
                return StatusCode(500, "Error retrieving job group.");
            }
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                return BadRequest("Name must be at least 2 characters long.");

            try
            {
                var jobGroups = await _unitOfWork.JobGroups.FindAsync(a => a.Name.ToLower().Contains(name.ToLower()));

                if (!jobGroups.Any())
                    return NotFound($"No JobGroup found with the name '{name}'.");

                BaseDTO output = _mapper.MapJobGroupToBaseDTO(jobGroups.First());
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching for JobGroups: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobGroupDTO jobGroupDto)
        {
            if (jobGroupDto == null)
                return BadRequest("Job group data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {           // Check if a JobGroup with the same name already exists
                var existingJobGroup = await _unitOfWork.JobGroups
                    .FindAsync(j => j.Name.ToLower() == jobGroupDto.Name.ToLower());

                if (existingJobGroup.Any())
                    return Conflict($"A job group with the name '{jobGroupDto.Name}' already exists.");

                // Map the DTO to the entity.
                JobGroup jobGroupEntity = _mapper.MapCreateBaseDTOToJobGroup(jobGroupDto);
                await _unitOfWork.JobGroups.AddAsync(jobGroupEntity);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetByID), new { id = jobGroupEntity.Id }, jobGroupDto);
            }
            catch
            {
                return StatusCode(500, "Error creating job group.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateBaseDTO jobGroupDto)
        {
            if (jobGroupDto == null)
                return BadRequest("Job group data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingJobGroup = await _unitOfWork.JobGroups.GetByIdAsync(id);

                if (existingJobGroup == null)
                    return NotFound($"Job group with ID {id} not found.");


                var duplicateJobGroup = await _unitOfWork.JobGroups
                    .FindAsync(j => j.Name.ToLower() == jobGroupDto.Name.ToLower() && j.Id != id);

                if (duplicateJobGroup.Any())
                    return Conflict($"A job group with the name '{jobGroupDto.Name}' already exists.");

                existingJobGroup.Code = jobGroupDto.Code;
                existingJobGroup.Name = jobGroupDto.Name;
                await _unitOfWork.SaveAsync();

                return Ok(jobGroupDto);
            }
            catch
            {
                return StatusCode(500, "Error updating job group.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            try
            {
                var jobGroup = await _unitOfWork.JobGroups.GetByIdAsync(id);

                if (jobGroup == null)
                    return NotFound($"Job group with ID {id} not found.");

                // Optional: Check for related entities before deletion
                var relatedJobSubGroup = await _unitOfWork.JobSubGroups.FindAsync(a => a.JobGroupsId == id);
                if (relatedJobSubGroup.Any())
                    return Conflict($"Cannot delete job group. {relatedJobSubGroup.Count()} Jobsubgroup are associated with this job group.");


                _unitOfWork.JobGroups.Delete(jobGroup.Id);
                await _unitOfWork.SaveAsync();

                return Ok($"Job Group with Id {id} deleted successfully");
            }
            catch
            {
                return StatusCode(500, "Error deleting job group.may be it is related With some Employees");
            }
        }
    }
}