using Microsoft.AspNetCore.Mvc;
using Server.Data.Entities;
using Server.Mapping.Profile;
using Server.Models.DTOs.PenaltyCase;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PenaltyCaseController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PenaltyCaseProfile profile;

    public PenaltyCaseController(IUnitOfWork unitOfWork , PenaltyCaseProfile profile)
    {
        _unitOfWork = unitOfWork;
        this.profile = profile;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var entitys = await _unitOfWork.PenaltyCases.GetAllAsync();

            if (entitys == null || !entitys.Any())
            {
                return NotFound("No PenaltyCases Found.!");

            }

            var getDTO = profile.PenaltyCaseList_To_GetPenaltyCaseDTOList(entitys);
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
            var entity  = await _unitOfWork.PenaltyCases.GetByIdAsync(id);
            
            if (entity == null)
                return NotFound($"!PenaltyCase With Id{id} NOT FOUND.");

            var GetDTO = profile.PenaltyCase_To_GetPenaltyCaseDTO(entity);
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


            var entities = await _unitOfWork.PenaltyCases.FindAsync(e => e.Name.ToLower().Contains(name.ToLower()));
            
            if (entities == null || !entities.Any())
                return NotFound($"No PenaltyCase found With Name{name}");

            var GetDTO = profile.PenaltyCaseList_To_GetPenaltyCaseDTOList(entities);
            return Ok(GetDTO);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An Error Occured {e.Message}");
        }
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PenaltyCaseDTO PenaltyCaseRequest)
    {
        if (PenaltyCaseRequest == null)
            return BadRequest("Penalty Case data is Required.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var IsExist = await _unitOfWork.PenaltyCases
                                 .GetByNameAsync(a => a.Name.ToLower() == PenaltyCaseRequest.Name.ToLower());
            
            if (IsExist != null)
                return Conflict($"A Penalty Case With The Name{PenaltyCaseRequest.Name} already exists.");

           
            var PenaltyCaseEntity = profile.PenaltyCaseDTO_To_PenaltyCase(PenaltyCaseRequest);

            await _unitOfWork.PenaltyCases.AddAsync(PenaltyCaseEntity);
            await _unitOfWork.SaveAsync();

            return Ok(PenaltyCaseRequest);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating: {e.Message}");
        }

    }



    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PenaltyCaseDTO penaltyCaseRequestDTO)
    {
        if (penaltyCaseRequestDTO == null)
            return BadRequest("Penalty Case data is Required.");
      
        if (id <= 0)
            return BadRequest("!Invalid Id.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var entity = await _unitOfWork.PenaltyCases.GetByIdAsync(id);

            if (entity == null)
                return BadRequest($"Penalty Case With id {id} not found. ");

            var duplicatePenaltyCase = await _unitOfWork
                                                 .PenaltyCases
                                                 .FindAsync(e => e.Name.ToLower() == penaltyCaseRequestDTO.Name.ToLower() && 
                                                 e.Code == penaltyCaseRequestDTO.Code 
                                                 );

            if (duplicatePenaltyCase.Any())
                return Conflict($"Penalty Case With The Same Code {penaltyCaseRequestDTO.Code} and Name {penaltyCaseRequestDTO.Name} already Exist");

            entity.Name = penaltyCaseRequestDTO.Name;
            entity.Code = penaltyCaseRequestDTO.Code;
            
            await _unitOfWork.SaveAsync();
            
            return Ok(penaltyCaseRequestDTO);

        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating: {e.Message}");
        }



    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete (int id)
    {
        if (id <= 0) return BadRequest("Invalid Id");

        try
        {
            var entity = await _unitOfWork.PenaltyCases.GetByIdAsync(id);

            if (entity == null)
                return NotFound($"Penalty Case with Id {id} not found.");

            _unitOfWork.PenaltyCases.Delete(entity.Id);
            await _unitOfWork.SaveAsync();

            return Ok("Deleted");
        }
        catch(Exception e)
        {
            return StatusCode(500, $"An error occurred while Deleting: {e.Message}");

        }
    }

}
