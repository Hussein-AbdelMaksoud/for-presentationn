using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Server.Data.Entities;
using Server.Models.DTOs.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> UserManager , IConfiguration config)
        {
            userManager = UserManager;
            this.config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register (RegisterDTO UserRequest)
        {
             if (ModelState.IsValid)
            {
                // save user to database 
                ApplicationUser user = new ApplicationUser();
                user.UserName = UserRequest.UserName;
                user.Email = UserRequest.Email;

                IdentityResult result =
                    await userManager.CreateAsync(user, UserRequest.Password);

                if (result.Succeeded)
                {
                    return Ok("Created");
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("User", item.Description);
                 }
                
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")] 
        public async Task<IActionResult> Login (LoginDTO UserRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser UserDB =
                    await userManager.FindByNameAsync(UserRequest.UserName);

                if (UserDB != null)
                {
                    bool UserFound =
                        await userManager.CheckPasswordAsync(UserDB, UserRequest.Password);

                    if (UserFound)
                    {
                        List<Claim> UserClaims = new List<Claim>();

                        UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, UserDB.Id));
                        UserClaims.Add(new Claim(ClaimTypes.Name, UserDB.UserName));

                        var UserRoles = await userManager.GetRolesAsync(UserDB);

                        foreach (var roleName in UserRoles)
                        {
                            UserClaims.Add(new Claim(ClaimTypes.Role, roleName));
                        }

                        var SecurityKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecretKey"]));

                        SigningCredentials SigningCred =
                            new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

                        // desing the token
                        JwtSecurityToken JwtToken = new JwtSecurityToken(
                            issuer:  config["JWT:IssuerIP"],
                            expires: DateTime.Now.AddHours(1),
                            claims: UserClaims,
                            signingCredentials: SigningCred

                            );

                        // generate token response 

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(JwtToken),
                            expiration = JwtToken.ValidTo
                        });
                    }
                  }

                    ModelState.AddModelError("UserName", "UserName or Password Invalid");
                }
                return BadRequest(ModelState);
            }
        }
}

