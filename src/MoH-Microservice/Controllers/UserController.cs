using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoH_Microservice.Models;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RoleUser")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("user-info")]
        public async Task<IActionResult> GetUserInfo([FromBody] string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var roles = await userManager.GetRolesAsync(user);

            return Ok(new { user.Id,user.UserName, user.Email, user.PhoneNumber, roles });
        }

        [HttpPut("user-info")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfo model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

           // user.Email = model.Email;
            user.UserName = model.Username;
            user.PhoneNumber = model.PhoneNumber;

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { message = "User Info Updated Successfully." });
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUserAccount([FromBody] string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { message = "User deleted successfully." });
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePassword model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new { message = "Password changed successfully." });
            }

            return BadRequest(result.Errors);
        }
    }
}
