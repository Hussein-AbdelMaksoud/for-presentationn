using Microsoft.AspNetCore.Mvc;
using Server.Data.Enums;

[ApiController]
[Route("api/[controller]")]
public class GendersController : ControllerBase
{
    // GET: api/genders
    [HttpGet]
    public IActionResult GetGenders()
    {
        // Option 1: Return just the names
        var genderNames = Enum.GetNames(typeof(Gender));
        return Ok(genderNames);

    }
}