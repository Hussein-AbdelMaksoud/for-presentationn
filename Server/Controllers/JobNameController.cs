using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Entities;
using Server.Mapping.Profile;
using Server.Services.UnitOfWork.Interfaces;
using Server.Models.DTOs.JobName;
using Server.Models.DTOs.JobSubGroup;
using Server.Models.DTOs;
using Server.Models.DTOs.MandateType;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobNamesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JobNameProfile _mapper;
        public JobNamesController(IUnitOfWork unitOfWork, JobNameProfile mapper)

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
                var jobNames = await _unitOfWork.JobNames.GetAllAsync();
                if (!jobNames.Any())
                    return NotFound("No JobName found.");

                // Use Mapperly to convert the entity list to a DTO list.
                IEnumerable<JobNameDTO> output = _mapper.MapJobNameListToJobNameDTOList(jobNames);
                return Ok(output);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving JobNames: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var jobName = await _unitOfWork.JobNames.GetByIdAsync(id);
                if (jobName == null)
                    return NotFound($"JobName with ID {id} not found.");

                JobNameDTO dto = _mapper.JobName_To_JobNameDTO(jobName);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the JobName: {ex.Message}");
            }
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                return BadRequest("Name must be at least 2 characters long.");

            try
            {
                var jobNames = await _unitOfWork.JobNames.FindAsync(a => a.Name.ToLower().Contains(name.ToLower()));

                if (!jobNames.Any())
                    return NotFound($"No JobNames found containing '{name}'.");

                IEnumerable<JobNameDTO> output = new List<JobNameDTO>();

                output = _mapper.MapJobNameListToJobNameDTOList(jobNames);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching for JobNames: {ex.Message}");
            }
        }

        //get by parent
        [HttpGet("ByJobSubGroup/{id}")]
        public async Task<IActionResult> GetJobNameByJobSubGroup(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");
            try
            {
                JobSubGroup jobSubGroups = await _unitOfWork.JobSubGroups.GetByIdAsync(id);
                if (jobSubGroups == null)
                    return NotFound($"JobSubGroup with ID {id} not found.");

                var jobNames = await _unitOfWork.JobNames.FindAsync(a => a.JobSubGroupId == id);
                if (jobNames == null || !jobNames.Any())
                    return NotFound($"No JobNames found for JobSubGroup with ID {id}.");

                IEnumerable<JobNameDTO> output = new List<JobNameDTO>();
                output = _mapper.MapJobNameListToJobNameDTOList(jobNames);
                return Ok(output);
            }
            catch
            {
                return StatusCode(500, $"An error occurred while retrieving JobName By JobSubGroup with ID {id}.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobNameDTO jobNameDto)
        {
            if (jobNameDto == null)
                return BadRequest("JobName data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Check if a Job Name with the same name already exists
                var existingJobName = await _unitOfWork.JobNames
                    .FindAsync(f => f.Name.ToLower() == jobNameDto.Name.ToLower());

                if (existingJobName.Any())
                    return Conflict($"A Job Name with the name '{jobNameDto.Name}' already exists.");

                // Validate JobSubGroupId
                if (jobNameDto.JobSubGroupId.HasValue)
                {
                    //check that jobSubGroupId can't be zero
                    if (jobNameDto.JobSubGroupId.Value == 0)
                        return BadRequest("JobSubGroupId cannot be zero.");

                    var jobSubGroupExists = await _unitOfWork.JobSubGroups.GetByIdAsync(jobNameDto.JobSubGroupId.Value);
                    if (jobSubGroupExists == null)
                        return BadRequest($"No Job Sub Group found with ID {jobNameDto.JobSubGroupId.Value}.JobSubGroupId must match an existing record in the database");
                }
                // Map the DTO to the entity.
                JobName jobNameEntity = _mapper.MapCreateJobNameDTOToJobName(jobNameDto);
                await _unitOfWork.JobNames.AddAsync(jobNameEntity);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = jobNameEntity.Id }, jobNameDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Job Name: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateJobNameDTO jobNameDto)
        {
            if (jobNameDto == null)
                return BadRequest("Job Name data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingJobName = await _unitOfWork.JobNames.GetByIdAsync(id);
                if (existingJobName == null)
                    return NotFound($"Job Name with ID {id} not found.");

                // Validate JobSubGroupId
                if (jobNameDto.JobSubGroupId.HasValue)
                {
                    //check that jobSubGroupId can't be zero
                    if (jobNameDto.JobSubGroupId.Value == 0)
                        return BadRequest("JobSubGroupId cannot be zero.");

                    var jobSubGroupExists = await _unitOfWork.JobSubGroups.GetByIdAsync(jobNameDto.JobSubGroupId.Value);
                    if (jobSubGroupExists == null)
                        return BadRequest($"No JobSubGroup found with ID {jobNameDto.JobSubGroupId.Value}.JobSubGroupId must match an existing record in the database");
                }

                // Update existing entity properties
                existingJobName.Name = jobNameDto.Name;
                existingJobName.JobMission = jobNameDto.JobMission;
                existingJobName.Code = jobNameDto.Code;

                // Optional: Add any additional special optional fields
                if (jobNameDto.JobSubGroupId.HasValue)
                    existingJobName.JobSubGroupId = jobNameDto.JobSubGroupId;
              
                await _unitOfWork.SaveAsync();

                return Ok(jobNameDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Job Name: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var jobName = await _unitOfWork.JobNames.GetByIdAsync(id);
                if (jobName == null)
                    return NotFound($"Job Name with ID {id} not found.");

                _unitOfWork.JobNames.Delete(jobName.Id);
                await _unitOfWork.SaveAsync();

                return Ok($"Job Name with Id {id} deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Job Name: {ex.Message},may be it is related With some Employees");
            }
        }

        #endregion
    }
}