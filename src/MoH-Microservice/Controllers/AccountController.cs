using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoH_Microservice.Models;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Login = MoH_Microservice.Models.Login;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody ] Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);

            if(userExists!=null)
            {
                return BadRequest(new { message = "User already exists" });
            }

            var user=new AppUser { UserName = model.Username ,Email=model.Email,PhoneNumber=model.PhoneNumber , UserType = model.UserType, Departement = model.Departement,Hospital=model.Hospital }; 
            var result=await _userManager.CreateAsync(user,model.Password);

            if (result.Succeeded)
            {
                return Ok(new {message="User registered successfully"});
            }

            return BadRequest(result.Errors);

        }

        [HttpPut("Login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    //new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                    new Claim("UserType", user.UserType!),
                    new Claim("Departement", user.Departement!),
                    new Claim("Hospital", user.Hospital!),
                    new Claim("userId", user.Id!),
                    

                };

                authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
                    SecurityAlgorithms.HmacSha256));

                await _userManager.SetAuthenticationTokenAsync(user, "Default",model.Username, new JwtSecurityTokenHandler().WriteToken(token));
                
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            

            return Unauthorized(new { message = "Invalid User name or passowrd." });

        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            if(!await _roleManager.RoleExistsAsync(role))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role));
                if (result.Succeeded)
                {
                    return Ok(new {message="Role added successfully"});
                }

                return BadRequest(result.Errors);
                    
            }
            return BadRequest("Role already Exists");
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null) {

                return BadRequest("User not found");
            }

            var result = await _userManager.AddToRoleAsync(user,model.Role);
            if (result.Succeeded) {
                return Ok(new { message = "Role assigned successfully" });
            }

            return Ok(result.Errors);   

        }

        [HttpGet("get-token")]
        public async Task<IActionResult> GetToken(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var token = await _userManager.GetAuthenticationTokenAsync(user, "Default", username);
            if (token == null)
            {
                return NotFound(new { message = "Token not found ,please login in agin." });
            }
            return Ok(token);
        }

        [HttpPost("reset-password-default")]
        public async Task<IActionResult> ResetPasswordToDefault([FromBody] ResetToDefaultDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
                return BadRequest("User not found.");

            // Remove the current password (if applicable)
            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
                return BadRequest(removePasswordResult.Errors);

            // Set a new default password
            var defaultPassword = "TsedeyBank@2025"; // Change this to your desired default password
            var addPasswordResult = await _userManager.AddPasswordAsync(user, defaultPassword);

            if (!addPasswordResult.Succeeded)
                return BadRequest(addPasswordResult.Errors);

            return Ok(new { Message = "Password reset to default successfully!" });
        }

        [HttpGet("check-login")]
        public async Task<IActionResult> CheckLogin([FromHeader] string? Authorization)
        {
            if (Authorization == null)
                return BadRequest("Invalid token");
            var tokens = new JwtSecurityToken(Authorization?.Split(" ")[1]);
            if (!(tokens.Payload.Exp >= DateTimeOffset.UtcNow.ToUnixTimeSeconds())) 
                return Unauthorized("No token / Token has expired!");

            return Ok();
        }
    }
}
