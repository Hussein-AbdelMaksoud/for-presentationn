using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Entities;
using Server.Mapping.Profile;
using Server.Models.DTOs.JobGroup;
using Server.Services.UnitOfWork.Interfaces;
using Server.Models.DTOs.JobSubGroup;
using Server.Models.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Server.Models.DTOs.JobName;
using System.Diagnostics.CodeAnalysis;

namespace Myproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSubGroupController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JobSubGroupProfile _mapper;

        public JobSubGroupController(IUnitOfWork unitOfWork, JobSubGroupProfile mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region EndPoints

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var jobSubGroups = await _unitOfWork.JobSubGroups.GetAllAsync();
                if (!jobSubGroups.Any())
                    return NotFound("No JobSubGroups found.");

                // Use Mapperly to convert the entity list to a DTO list.

                IEnumerable<JobSubGroupDTO> dtos = new List<JobSubGroupDTO>();

                dtos = _mapper.MapJobSubGroupListToJobSubGroupDTOList(jobSubGroups);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving JobSubGroups: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var jobSubGroup = await _unitOfWork.JobSubGroups.GetByIdAsync(id);
                if (jobSubGroup == null)
                    return NotFound($"JobSubGroup with ID {id} not found.");

                JobSubGroupDTO dto = new JobSubGroupDTO();
                dto = _mapper.JobSubGroup_To_JobSubGroupDTO(jobSubGroup);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the JobSubGroup: {ex.Message}");
            }
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                return BadRequest("Name must be at least 2 characters long.");

            try
            {
                var jobSubGroups = await _unitOfWork.JobSubGroups.FindAsync(a => a.Name.ToLower().Contains(name.ToLower()));

                if (!jobSubGroups.Any())
                    return NotFound($"No JobSubGroups found containing '{name}'.");

                IEnumerable<JobSubGroupDTO> output = new List<JobSubGroupDTO>();
                output = _mapper.MapJobSubGroupListToJobSubGroupDTOList(jobSubGroups);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching for JobSubGroups: {ex.Message}");
            }
        }
        //get by parent
        [HttpGet("ByJobGroup/{id}")]
        public async Task<IActionResult> GetJobSubGroupByJobGroup(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");
            try
            {
                JobGroup jobGroups = await _unitOfWork.JobGroups.GetByIdAsync(id);
                if (jobGroups == null)
                    return NotFound($"JobGroup with ID {id} not found.");

                var jobSubGroups = await _unitOfWork.JobSubGroups.FindAsync(a => a.JobGroupsId == id);
                if (jobSubGroups == null || !jobSubGroups.Any())
                    return NotFound($"No JobSubGroups found for JobGroup with ID {id}.");

                IEnumerable<JobSubGroupDTO> output = new List<JobSubGroupDTO>();
                output = _mapper.MapJobSubGroupListToJobSubGroupDTOList(jobSubGroups);
                return Ok(output);
            }
            catch
            {
                return StatusCode(500, $"An error occurred while retrieving JobSubGroup By JobGroup with ID {id}.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobSubGroupDTO jobSubGroupDto)
        {
            if (jobSubGroupDto == null)
                return BadRequest("JobSubGroup data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {    // Check if a JobSubGroup with the same name already exists
                var existingJobSubGroup = await _unitOfWork.JobSubGroups
                    .FindAsync(j => j.Name.ToLower() == jobSubGroupDto.Name.ToLower());

                if (existingJobSubGroup.Any())
                    return Conflict($"A job sub group with the name '{jobSubGroupDto.Name}' already exists.");
              
             
                if (jobSubGroupDto.JobGroupsId.HasValue)
                {
                    //check that jobGroupId can't be zero
                    if (jobSubGroupDto.JobGroupsId == 0)
                        return BadRequest("JobGroupId cannot be zero.");

                    var jobGroupExists = await _unitOfWork.JobGroups.GetByIdAsync(jobSubGroupDto.JobGroupsId.Value);
                    if (jobGroupExists == null)
                        return BadRequest($"No JobGroup found with ID {jobSubGroupDto.JobGroupsId.Value}.JobGroupId must match an existing record in the database");
                }
                // Map the DTO to the entity.
                JobSubGroup jobSubGroupEntity = _mapper.MapCreateJobSubGroupDTOToJobSubGroup(jobSubGroupDto);
                await _unitOfWork.JobSubGroups.AddAsync(jobSubGroupEntity);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = jobSubGroupEntity.Id }, jobSubGroupDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the JobSubGroup: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateJobSubGroupDTO jobSubGroupDto)
        {
            if (jobSubGroupDto == null)
                return BadRequest("JobSubGroup data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingJobSubGroup = await _unitOfWork.JobSubGroups.GetByIdAsync(id);
                if (existingJobSubGroup == null)
                    return NotFound($"JobSubGroup with ID {id} not found.");
               
               
                if (jobSubGroupDto.JobGroupsId.HasValue)
                {
                    //check that jobSubGroupId can't be zero
                    if (jobSubGroupDto.JobGroupsId.Value == 0)
                        return BadRequest("JobGroupsId cannot be zero.");

                    var jobGroupExists = await _unitOfWork.JobGroups.GetByIdAsync(jobSubGroupDto.JobGroupsId.Value);
                    if (jobGroupExists == null)
                        return BadRequest($"No JobGroup found with ID {jobSubGroupDto.JobGroupsId.Value}.JobGroupId must match an existing record in the database");
                }
                existingJobSubGroup.Code = jobSubGroupDto.Code;
                existingJobSubGroup.Name = jobSubGroupDto.Name;
                existingJobSubGroup.JobGroupsId = jobSubGroupDto.JobGroupsId;

                await _unitOfWork.SaveAsync();

                return Ok(jobSubGroupDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the JobSubGroup: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var jobSubGroup = await _unitOfWork.JobSubGroups.GetByIdAsync(id);
                if (jobSubGroup == null)
                    return NotFound($"JobSubGroup with ID {id} not found.");

                // Optional: Check for related entities before deletion
                var relatedJobName = await _unitOfWork.JobNames.FindAsync(a => a.JobSubGroupId == id);
                if (relatedJobName.Any())
                    return Conflict($"Cannot delete jobSubGroup . {relatedJobName.Count()} JobName are associated with this job group.");

                _unitOfWork.JobSubGroups.Delete(jobSubGroup.Id);
                await _unitOfWork.SaveAsync();

                return Ok($"Job Sub Group with Id {id} deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the JobSubGroup: {ex.Message}, may be it is related With some Employees");
            }
        }

        #endregion
    }
}
