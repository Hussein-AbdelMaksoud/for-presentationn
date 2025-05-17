using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Entities;
using Server.Mapping.Profile;
using Server.Models.DTOs.Department;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DepartmentProfile _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, DepartmentProfile departmentProfile)
        {
            _unitOfWork = unitOfWork;
            _mapper = departmentProfile;
        }

        #region EndPoints

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var departments = await _unitOfWork.Departments.GetAllAsync();

                if (departments == null || !departments.Any())
                    return NotFound("No departments found.");
                
                IEnumerable<DepartmentDTO> output = _mapper.DepartmentList_To_DepartmentDTOList(departments);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving departments: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(id);

                if (department == null)
                    return NotFound($"Department with ID {id} not found.");

                DepartmentDTO output = _mapper.Department_To_DepartmentDTO(department);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the department: {ex.Message}");
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
                var departments = await _unitOfWork.Departments.FindAsync(d => d.Name.ToLower().Contains(lowerCaseName));

                if (!departments.Any())
                    return NotFound($"No departments found containing '{name}'.");

                IEnumerable<DepartmentDTO> output = _mapper.DepartmentList_To_DepartmentDTOList(departments);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching for departments: {ex.Message}");
            }
        }

        [HttpGet("BySubAd/{id}")]
        public async Task<IActionResult> GetBySubAd(int id) 
        { 
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");
            try
            {
                SubAd subAd = await _unitOfWork.SubAds.GetByIdAsync(id);
                if (subAd == null)
                    return NotFound($"SubAd with ID {id} not found.");

                var departments = await _unitOfWork.Departments.FindAsync(a => a.SubAdID == id);
                if (departments == null || !departments.Any())
                    return NotFound($"No departments found for subAd with ID {id}.");

                IEnumerable<DepartmentDTO> output = _mapper.DepartmentList_To_DepartmentDTOList(departments);
                return Ok(output);
            }
            catch
            {
                return StatusCode(500, $"An error occurred while retrieving departments by subAd with ID {id}.");
            }
        }

        

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDTO department)
        {
            if (department == null)
                return BadRequest("Department data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(department.SubAdID <= 0)            
                return BadRequest("SubAd ID is must be a positive number.");      
            
            if(_unitOfWork.SubAds.GetByIdAsync((int)department.SubAdID).Result == null)            
                return BadRequest($"SubAd with ID {department.SubAdID} not found.");            
            try
            {
                // Check if a department with the same name already exists
                var existingDepartment = await _unitOfWork.Departments.FindAsync(d => d.Name.ToLower() == department.Name.ToLower());

                if (existingDepartment.Any())
                    return Conflict($"A department with the name '{department.Name}' already exists.");

                Department dept = _mapper.CreateDepartment_To_Department(department);
                await _unitOfWork.Departments.AddAsync(dept);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetById), new { id = dept.Id }, department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the department: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int Id,[FromBody] CreateDepartmentDTO department)
        {
            if (department == null)
                return BadRequest("Department data is required.");

            if (Id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (department.SubAdID <= 0)
                return BadRequest("SubAd ID is must be a positive number.");

            if (_unitOfWork.SubAds.GetByIdAsync((int)department.SubAdID).Result == null)
                return BadRequest($"SubAd with ID {department.SubAdID} not found.");
            try
            {
                var existingDepartment = await _unitOfWork.Departments.GetByIdAsync(Id);

                if (existingDepartment == null)
                    return NotFound($"Department with ID {Id} not found.");

                // Check if another department with the same name exists
                var duplicateDepartment = await _unitOfWork.Departments.FindAsync(d => d.Name.ToLower() == department.Name.ToLower() && d.Code != department.Code);

                if (duplicateDepartment.Any())
                    return Conflict($"A department with the name '{department.Name}' already exists.");

                existingDepartment.Name = department.Name;
                existingDepartment.Status = department.Status;
                existingDepartment.SubAdID = department.SubAdID;
                await _unitOfWork.SaveAsync();

                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the department: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID. ID must be a positive number.");

            try
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(id);

                if (department == null)
                    return NotFound($"Department with ID {id} not found.");             

                _unitOfWork.Departments.Delete(id);
                await _unitOfWork.SaveAsync();

                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the department: {ex.Message}");
            }
        }

        #endregion
    }
}
