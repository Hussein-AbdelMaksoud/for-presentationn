using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Entities;
using Server.Mapping.Profile;
using Server.Models.DTOs.Training;
using Server.Services.UnitOfWork.Interfaces;
using System.Collections.Generic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TrainingProfile _mapper;

        public TrainingController(IUnitOfWork unitOfWork, TrainingProfile trainingProfile)
        {
            _unitOfWork = unitOfWork;
            _mapper = trainingProfile;
        }

        #region EndPoints

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Trainings = await _unitOfWork.Trainings.FindAsync(a=>a.Id>0 , ["Employee"]);

                if (Trainings == null || !Trainings.Any())
                    return NotFound("No Trainings found.");

                IEnumerable<TrainingDTO> output = _mapper.TrainingList_To_TrainingDTOList(Trainings);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Trainings: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var Training = await _unitOfWork.Trainings.FindAsync(a=> a.Id == id, ["Employee"]);

                if (Training == null)
                    return NotFound($"Training with ID {id} not found.");

                IEnumerable<TrainingDTO> output = _mapper.TrainingList_To_TrainingDTOList(Training);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the Training: {ex.Message}");
            }
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                return BadRequest("Name must be at least 2 characters long.");

            try
            {
                var lowerCaseName = name.ToLower();
                var Trainings = await _unitOfWork.Trainings.FindAsync(d => d.CourseName.ToLower().Contains(lowerCaseName), ["Employee"]);

                if (!Trainings.Any())
                    return NotFound($"No Trainings found containing '{name}'.");

                IEnumerable<TrainingDTO> output = _mapper.TrainingList_To_TrainingDTOList(Trainings);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching for Trainings: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTrainingDTO Training)
        {
            if (Training == null)
                return BadRequest("Training data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(Training.StartDate > Training.EndDate)
            {
                return BadRequest("StartDate must be before EndDate.");
            }
            try
            {
                Training dept = _mapper.CreateTraining_To_Training(Training);
                await _unitOfWork.Trainings.AddAsync(dept);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = dept.Id }, Training);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Training: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int Id, [FromBody] CreateTrainingDTO Training)
        {
            if (Training == null)
                return BadRequest("Training data is required.");

            if (Id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Training.StartDate > Training.EndDate)
            {
                return BadRequest("StartDate must be before EndDate.");
            }

            try
            {
                var existingTraining = await _unitOfWork.Trainings.GetByIdAsync(Id);

                if (existingTraining == null)
                    return NotFound($"Training with ID {Id} not found.");

                existingTraining.CourseName = Training.CourseName;
                existingTraining.StartDate = Training.StartDate;
                existingTraining.EndDate = Training.EndDate;
                existingTraining.Notes = Training.Notes;
                existingTraining.Grade = Training.Grade;
                existingTraining.Type = Training.Type;
                existingTraining.Place = Training.Place;
                existingTraining.NationalId = Training.NationalId;
                await _unitOfWork.SaveAsync();

                return Ok(Training);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Training: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var Training = await _unitOfWork.Trainings.GetByIdAsync(id);

                if (Training == null)
                    return NotFound($"Training with ID {id} not found.");

                _unitOfWork.Trainings.Delete(id);
                await _unitOfWork.SaveAsync();

                return Ok(Training);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Training: {ex.Message}");
            }
        }

        #endregion
    }
}
