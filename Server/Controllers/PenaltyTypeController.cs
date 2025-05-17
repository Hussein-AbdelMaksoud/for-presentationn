using Microsoft.AspNetCore.Mvc;
using Server.Mapping.Profile;
using Server.Models.DTOs.PenaltyType;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PenaltyTypeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PenaltyTypeProfile profile;

    public PenaltyTypeController(IUnitOfWork unitOfWork, PenaltyTypeProfile profile)
    {
        _unitOfWork = unitOfWork;
        this.profile = profile;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var entitys = await _unitOfWork.PenaltyTypes.GetAllAsync();

            if (entitys == null || !entitys.Any())
            {
                return NotFound("No Penalty Types Found.!");

            }

            var getDTO = profile.PenaltyTypeList_To_GetPenaltyTypeDTOList(entitys);
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
            var entity = await _unitOfWork.PenaltyTypes.GetByIdAsync(id);

            if (entity == null)
                return NotFound($"!Penalty Type With Id{id} NOT FOUND.");

            var GetDTO = profile.PenaltyType_To_GetPenaltyTypeDTO(entity);
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


            var entities = await _unitOfWork.PenaltyTypes.FindAsync(e => e.Name.ToLower().Contains(name.ToLower()));

            if (entities == null || !entities.Any())
                return NotFound($"No Penalty Types found With Name{name}");

            var GetDTO = profile.PenaltyTypeList_To_GetPenaltyTypeDTOList(entities);
            return Ok(GetDTO);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An Error Occured {e.Message}");
        }
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PenaltyTypeDTO PenaltyTypeRequest)
    {
        if (PenaltyTypeRequest == null)
            return BadRequest("Penalty_Type data is Required.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var IsExist = await _unitOfWork.PenaltyTypes
                                 .GetByNameAsync(a => a.Name.ToLower() == PenaltyTypeRequest.Name.ToLower());

            if (IsExist != null)
                return Conflict($"A Penalty_Type With The Name{PenaltyTypeRequest.Name} already exists.");


            var entity = profile.PenaltyTypeDTO_To_PenaltyType(PenaltyTypeRequest);

            await _unitOfWork.PenaltyTypes.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            return Ok(PenaltyTypeRequest);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating: {e.Message}");
        }

    }



    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PenaltyTypeDTO PenaltyTypeRequestDTO)
    {
        if (PenaltyTypeRequestDTO == null)
            return BadRequest("Penalty_Type data is Required.");

        if (id <= 0)
            return BadRequest("!Invalid Id.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var entity = await _unitOfWork.PenaltyTypes.GetByIdAsync(id);

            if (entity == null)
                return BadRequest($"Penalty_Type With id {id} not found. ");

            var duplicateEntity = await _unitOfWork
                                                 .PenaltyTypes
                                                 .FindAsync(e => e.Name.ToLower() == PenaltyTypeRequestDTO.Name.ToLower() &&
                                                 e.Code == PenaltyTypeRequestDTO.Code
                                                 );

            if (duplicateEntity.Any())
                return Conflict($"Penalty_Type With The Same Code {PenaltyTypeRequestDTO.Code} and Name {PenaltyTypeRequestDTO.Name} already Exist");

            entity.Name = PenaltyTypeRequestDTO.Name;
            entity.Code = PenaltyTypeRequestDTO.Code;

            await _unitOfWork.SaveAsync();

            return Ok(PenaltyTypeRequestDTO);

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
            var entity = await _unitOfWork.PenaltyTypes.GetByIdAsync(id);

            if (entity == null)
                return NotFound($"Penalty_Type with Id {id} not found.");

            _unitOfWork.PenaltyTypes.Delete(entity.Id);
            await _unitOfWork.SaveAsync();

            return Ok("Deleted");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating: {e.Message}");

        }
    }

}
