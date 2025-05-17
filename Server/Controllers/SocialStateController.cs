using Microsoft.AspNetCore.Mvc;
using Server.Mapping.Profile;
using Server.Models.DTOs.SocialState;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SocialStateController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly SocialStateProfile profile;

    public SocialStateController(IUnitOfWork unitOfWork, SocialStateProfile profile)
    {
        _unitOfWork = unitOfWork;
        this.profile = profile;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var entitys = await _unitOfWork.SocialStates.GetAllAsync();

            if (entitys == null || !entitys.Any())
            {
                return NotFound("No SocialStates  Found.!");

            }

            var getDTO = profile.SocialStateList_To_GetSocialStateDTOList(entitys);
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
            var entity = await _unitOfWork.SocialStates.GetByIdAsync(id);

            if (entity == null)
                return NotFound($"!SocialState With Id{id} NOT FOUND.");

            var GetDTO = profile.SocialState_To_GetSocialStateDTO(entity);
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


            var entities = await _unitOfWork.SocialStates.FindAsync(e => e.Name.ToLower().Contains(name.ToLower()));

            if (entities == null || !entities.Any())
                return NotFound($"No SocialStates found With Name{name}");

            var GetDTO = profile.SocialStateList_To_GetSocialStateDTOList(entities);
            return Ok(GetDTO);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An Error Occured {e.Message}");
        }
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SocialStateDTO SocialStateRequestDTO)
    {
        if (SocialStateRequestDTO == null)
            return BadRequest("SocialState data is Required.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var IsExist = await _unitOfWork.SocialStates
                                 .GetByNameAsync(a => a.Name.ToLower() == SocialStateRequestDTO.Name.ToLower());

            if (IsExist != null)
                return Conflict($"A SocialState  With The Name{SocialStateRequestDTO.Name} already exists.");


            var entity = profile.SocialStateDTO_To_SocialState(SocialStateRequestDTO);

            await _unitOfWork.SocialStates.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            return Ok(SocialStateRequestDTO);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating: {e.Message}");
        }

    }



    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SocialStateDTO SocialStateRequestDTO)
    {
        if (SocialStateRequestDTO == null)
            return BadRequest("SocialState data is Required.");

        if (id <= 0)
            return BadRequest("!Invalid Id.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var entity = await _unitOfWork.SocialStates.GetByIdAsync(id);

            if (entity == null)
                return BadRequest($"SocialState With id {id} not found. ");

            var duplicateEntity = await _unitOfWork
                                                 .SocialStates
                                                 .FindAsync(e => e.Name.ToLower() == SocialStateRequestDTO.Name.ToLower() &&
                                                 e.Code == SocialStateRequestDTO.Code
                                                 );

            if (duplicateEntity.Any())
                return Conflict($"SocialState With The Same Code {SocialStateRequestDTO.Code} and Name {SocialStateRequestDTO.Name} already Exist");

            entity.Name = SocialStateRequestDTO.Name;
            entity.Code = SocialStateRequestDTO.Code;

            await _unitOfWork.SaveAsync();

            return Ok(SocialStateRequestDTO);

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
            var entity = await _unitOfWork.SocialStates.GetByIdAsync(id);

            if (entity == null)
                return NotFound($"SocialState with Id {id} not found.");

            _unitOfWork.SocialStates.Delete(entity.Id);
            await _unitOfWork.SaveAsync();

            return Ok("Deleted");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating: {e.Message}");

        }
    }

}
