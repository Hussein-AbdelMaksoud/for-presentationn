using Microsoft.AspNetCore.Mvc;
using Server.Mapping.Profile;
using Server.Models.DTOs.QualGrade;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QualGradeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly QualGradeProfile profile;

    public QualGradeController(IUnitOfWork unitOfWork, QualGradeProfile profile)
    {
        _unitOfWork = unitOfWork;
        this.profile = profile;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var entitys = await _unitOfWork.QualGrades.GetAllAsync();

            if (entitys == null || !entitys.Any())
            {
                return NotFound("No QualGrades  Found.!");

            }

            var getDTO = profile.QualGradeList_To_GetQualGradeDTOList(entitys);
            return Ok(getDTO);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An Error Occured {ex.Message}");
        }
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("!Invalid Id.");
        try
        {
            var entity = await _unitOfWork.QualGrades.GetByIdAsync(id);

            if (entity == null)
                return NotFound($"!QualGrade With Id{id} NOT FOUND.");

            var GetDTO = profile.QualGrade_To_GetQualGradeDTO(entity);
            return Ok(GetDTO);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An Error Occured {e.Message}");
        }
    }


    [HttpGet("byname/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Invalid name.");

        try
        {


            var entities = await _unitOfWork.QualGrades.FindAsync(e => e.Name.ToLower().Contains(name.ToLower()));

            if (entities == null || !entities.Any())
                return NotFound($"No QualGrades found With Name{name}");

            var GetDTO = profile.QualGradeList_To_GetQualGradeDTOList(entities);
            return Ok(GetDTO);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An Error Occured {e.Message}");
        }
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] QualGradeDTO QualGradeRequestDTO)
    {
        if (QualGradeRequestDTO == null)
            return BadRequest("QualGrade data is Required.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var IsExist = await _unitOfWork.QualGrades
                                 .GetByNameAsync(a => a.Name.ToLower() == QualGradeRequestDTO.Name.ToLower());

            if (IsExist != null)
                return Conflict($"A QualGrade  With The Name{QualGradeRequestDTO.Name} already exists.");


            var entity = profile.QualGradeDTO_To_QualGrade(QualGradeRequestDTO);

            await _unitOfWork.QualGrades.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            return Ok(QualGradeRequestDTO);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating: {e.Message}");
        }

    }



    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] QualGradeDTO QualGradeRequestDTO)
    {
        if (QualGradeRequestDTO == null)
            return BadRequest("QualGrade  data is Required.");

        if (id <= 0)
            return BadRequest("!Invalid Id.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var entity = await _unitOfWork.QualGrades.GetByIdAsync(id);

            if (entity == null)
                return BadRequest($"QualGrade With id {id} not found. ");

            var duplicateEntity = await _unitOfWork
                                                 .QualGrades
                                                 .FindAsync(e => e.Name.ToLower() == QualGradeRequestDTO.Name.ToLower() &&
                                                 e.Code == QualGradeRequestDTO.Code
                                                 );

            if (duplicateEntity.Any())
                return Conflict($"QualGrade With The Same Code {QualGradeRequestDTO.Code} and Name {QualGradeRequestDTO.Name} already Exist");

            entity.Name = QualGradeRequestDTO.Name;
            entity.Grade = QualGradeRequestDTO.Grade;
            entity.Code = QualGradeRequestDTO.Code;

            await _unitOfWork.SaveAsync();

            return Ok(QualGradeRequestDTO);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating: {e.Message}");
        }



    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest("Invalid Id");

        try
        {
            var entity = await _unitOfWork.QualGrades.GetByIdAsync(id);

            if (entity == null)
                return NotFound($"QualGrade with Id {id} not found.");

            _unitOfWork.QualGrades.Delete(entity.Id);
            await _unitOfWork.SaveAsync();

            return Ok("Deleted");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating: {e.Message}");

        }
    }

}
