using Microsoft.AspNetCore.Mvc;
using Server.Mapping.Profile;
using Server.Models.DTOs.VacationType;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacationTypeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly VacationTypeProfile profile;

    public VacationTypeController(IUnitOfWork unitOfWork, VacationTypeProfile profile)
    {
        _unitOfWork = unitOfWork;
        this.profile = profile;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var entitys = await _unitOfWork.VacationTypes.GetAllAsync();

            if (entitys == null || !entitys.Any())
            {
                return NotFound("No VacationTypes  Found.!");

            }

            var getDTO = profile.VacationTypeList_To_GetVacationTypeDTOList(entitys);
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
            var entity = await _unitOfWork.VacationTypes.GetByIdAsync(id);

            if (entity == null)
                return NotFound($"!VacationType With Id{id} NOT FOUND.");

            var GetDTO = profile.VacationType_To_GetVacationDTO(entity);
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


            var entities = await _unitOfWork.VacationTypes.FindAsync(e => e.Name.ToLower().Contains(name.ToLower()));

            if (entities == null || !entities.Any())
                return NotFound($"No VacationTypes found With Name{name}");

            var GetDTO = profile.VacationTypeList_To_GetVacationTypeDTOList(entities);
            return Ok(GetDTO);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An Error Occured {e.Message}");
        }
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VacationTypeDTO VacationTypesRequestDTO)
    {
        if (VacationTypesRequestDTO == null)
            return BadRequest("VacationTypes data is Required.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var IsExist = await _unitOfWork.VacationTypes
                                 .GetByNameAsync(a => a.Name.ToLower() == VacationTypesRequestDTO.Name.ToLower());

            if (IsExist != null)
                return Conflict($"A VacationType With The Name{VacationTypesRequestDTO.Name} already exists.");


            var entity = profile.VacationTypeDTO_To_VacationType(VacationTypesRequestDTO);

            await _unitOfWork.VacationTypes.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            return Ok(VacationTypesRequestDTO);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating: {e.Message}");
        }

    }



    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] VacationTypeDTO VacationTypeRequestDTO)
    {
        if (VacationTypeRequestDTO == null)
            return BadRequest("VacationType data is Required.");

        if (id <= 0)
            return BadRequest("!Invalid Id.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var entity = await _unitOfWork.VacationTypes.GetByIdAsync(id);

            if (entity == null)
                return BadRequest($"VacationType With id {id} not found. ");

            var duplicateEntity = await _unitOfWork
                                                 .VacationTypes
                                                 .FindAsync(e => e.Name.ToLower() == VacationTypeRequestDTO.Name.ToLower() &&
                                                 e.Code == VacationTypeRequestDTO.Code
                                                 );

            if (duplicateEntity.Any())
                return Conflict($"VacationType With The Same Code {VacationTypeRequestDTO.Code} and Name {VacationTypeRequestDTO.Name} already Exist");

            entity.Name = VacationTypeRequestDTO.Name;
            entity.Code = VacationTypeRequestDTO.Code;

            await _unitOfWork.SaveAsync();

            return Ok(VacationTypeRequestDTO);

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
            var entity = await _unitOfWork.VacationTypes.GetByIdAsync(id);

            if (entity == null)
                return NotFound($"VacationType with Id {id} not found.");

            _unitOfWork.VacationTypes.Delete(entity.Id);
            await _unitOfWork.SaveAsync();

            return Ok("Deleted");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating: {e.Message}");

        }
    }


}
