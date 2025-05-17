using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.JobType;
using Server.Models.Profile;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JobTypeProfile _profile;

        public JobTypeController(IUnitOfWork unitOfWork, JobTypeProfile profile)
        {
            _unitOfWork = unitOfWork;
            _profile = profile;
        }

        #region Endpoints

        /// <summary>
        /// GET: api/JobType
        /// - **Description**: Retrieves all JobType items from the database.
        /// - **URL**: `GET api/JobType`
        /// - **Response**: A list of all JobTypes.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var jobTypes = await _unitOfWork.JobTypes.GetAllAsync();
                if (!jobTypes.Any())
                    return NotFound("No Job Types found.");

                var jobTypeDtos = _profile.ToDTOs(jobTypes);
                return Ok(jobTypeDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Job Types: {ex.Message}");
            }
        }

        /// <summary>
        /// GET: api/JobType/{id}
        /// - **Description**: Retrieves a specific JobType by its ID.
        /// - **URL**: `GET api/JobType/{id}`
        /// - **Response**: The JobType item with the specified ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. Please provide a valid positive ID.");

            try
            {
                var jobType = await _unitOfWork.JobTypes.GetByIdAsync(id);
                if (jobType == null)
                    return NotFound($"Job Type with ID {id} not found.");

                var jobTypeDto = _profile.ToDTO(jobType);
                return Ok(jobTypeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the Job Type: {ex.Message}");
            }
        }

        /// <summary>
        /// GET: api/JobType/by-name/{name}
        /// - **Description**: Searches for JobType items containing the specified name.
        /// - **URL**: `GET api/JobType/by-name/{name}`
        /// - **Response**: A list of JobTypes that contain the given name.
        /// </summary>
        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name cannot be empty.");

            name = name.Trim();

            try
            {
                var jobTypes = await _unitOfWork.JobTypes.FindAsync(jt => jt.Name.ToLower().Contains(name.ToLower()));

                if (!jobTypes.Any())
                    return NotFound($"No Job Type found with name '{name}'.");

                var jobTypeDtos = _profile.ToDTOs(jobTypes);
                return Ok(jobTypeDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Job Type by name: {ex.Message}");
            }
        }

        /// <summary>
        /// POST: api/JobType
        /// - **Description**: Creates a new JobType item.
        /// - **URL**: `POST api/JobType`
        /// - **Request Body**: JSON object containing JobType data.
        /// - **Response**: The created JobType item with its assigned ID.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobTypeDTO jobTypeDto)
        {
            if (jobTypeDto == null)
                return BadRequest("Job Type data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string jobTypeName = jobTypeDto.Name.Trim();

                var existingJobType = await _unitOfWork.JobTypes.FindAsync(jt => jt.Name.ToLower() == jobTypeName.ToLower());

                if (existingJobType.Any())
                    return Conflict($"A Job Type with Name {jobTypeDto.Name} already exists.");

                var jobType = _profile.ToCreateEntity(jobTypeDto);

                await _unitOfWork.JobTypes.AddAsync(jobType);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = jobType.Id }, _profile.ToDTO(jobType));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Job Type: {ex.Message}");
            }
        }

        /// <summary>
        /// PUT: api/JobType/{id}  
        /// - **Description**: Updates an existing JobType item by ID.  
        /// - **URL**: `PUT api/JobType/{id}`  
        /// - **Request Body**: JSON object containing updated JobType data (Name, Code).  
        /// - **Response**: Returns the updated JobType item.  
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateJobTypeDTO jobTypeDto)
        {
            if (jobTypeDto == null)
                return BadRequest("Job Type data is required.");

            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingJobType = await _unitOfWork.JobTypes.GetByIdAsync(id);
                if (existingJobType == null)
                    return NotFound($"Job Type with ID {id} not found.");

                string jobTypeName = jobTypeDto.Name.Trim();
                var duplicateJobType = await _unitOfWork.JobTypes.FindAsync(
                    jt => jt.Name.ToLower() == jobTypeName.ToLower() && jt.Id != id
                );

                if (duplicateJobType.Any())
                    return Conflict($"A Job Type with the name '{jobTypeDto.Name}' already exists.");

                existingJobType.Name = jobTypeName;
                existingJobType.Code = jobTypeDto.Code;

                await _unitOfWork.SaveAsync();

                var updatedJobTypeDTO = _profile.ToDTO(existingJobType);
                return Ok(updatedJobTypeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Job Type: {ex.Message}");
            }
        }

        /// <summary>
        /// DELETE: api/JobType/{id}
        /// - **Description**: Deletes a JobType item by its ID.
        /// - **URL**: `DELETE api/JobType/{id}`
        /// - **Response**: Success message if the item is deleted.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var jobType = await _unitOfWork.JobTypes.GetByIdAsync(id);
                if (jobType == null)
                    return NotFound($"Job Type with ID {id} not found.");

                _unitOfWork.JobTypes.Delete(jobType.Id);
                await _unitOfWork.SaveAsync();

                return Ok("Job Type deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Job Type: {ex.Message}");
            }
        }

        #endregion
    }
}
