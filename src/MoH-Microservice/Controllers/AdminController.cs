using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoH_Microservice.Models;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userManager.Users.ToListAsync();

            var usersWithRoles = new List<object>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                usersWithRoles.Add(new
                {
                    Id = user.Id,
                    Username=user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Departement=user.Departement,
                    UserType=user.UserType,
                    Roles = roles
                });
            }

            return Ok(usersWithRoles);
        }


        [HttpPost("users")]
        public async Task<IActionResult> AddUser([FromBody] Register model)
        {

            var userExists = await userManager.FindByNameAsync(model.Username);

            if (userExists != null)
            {
                return BadRequest(new { message = "User already exists" });
            }

            var user = new AppUser { UserName = model.Username, Email = model.Email, PhoneNumber = model.PhoneNumber,Departement=model.Departement,UserType=model.UserType};
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole("User"));
                    if (roleResult.Succeeded)
                    {
                        await userManager.DeleteAsync(user);
                        return StatusCode(500, new { message = "User role creation failed.", errors = roleResult.Errors });
                    }
                }

                await userManager.AddToRoleAsync(user, "User");
                return Ok(new { message = "User Added Successfully." });
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("users/{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)  
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (isAdmin)
            {
                return BadRequest(new { message = "Admin cannot be deleted." });
            }

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { message = "User deleted successfully." });
            }

            return BadRequest(result.Errors);
        }


        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await roleManager.Roles.Select(e => new { e.Id, e.Name }).ToListAsync();
            return Ok(roles);
        }

        [HttpPost("roles")]
        public async Task<IActionResult> AddNewRole([FromBody] string roleName)
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                return BadRequest("Role already exists.");
            }
            var result = await roleManager.CreateAsync(new IdentityRole { Name = roleName });
            if (result.Succeeded)
            {
                return Ok(new { message = "Role added successfully." });
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("roles")]
        public async Task<IActionResult> DeleteRole([FromBody] string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            if (role.Name == "Admin")
            {
                return BadRequest(new { message = "Admin role cannot be deleted." });
            }

            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role Deleted Successfully." });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("change-user-role")]
        public async Task<IActionResult> ChangeUserRole([FromBody] ChangeRole model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return NotFound($"User  {model.UserName} not found.");
            }

            var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (isAdmin)
            {
                return BadRequest(new { message = "Admin role cannot be changed." });
            }

            if (!await roleManager.RoleExistsAsync(model.NewRole))
            {
                return BadRequest($"Role {model.NewRole} does not exists.");
            }

            var currentRoles = await userManager.GetRolesAsync(user);
            var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!removeResult.Succeeded)
            {
                return BadRequest("Failed to remove user's current role");
            }

            var addResult = await userManager.AddToRoleAsync(user, model.NewRole);
            if (addResult.Succeeded)
            {
                return Ok($"User {model.UserName} role changes to {model.NewRole} successfully.");
            }

            return BadRequest("Failed to add user to the new role.");
        }

        [HttpPost("admin-info")]
        public async Task<IActionResult> GetAdminInfo([FromBody] string username)
        {
            var admin = await userManager.FindByNameAsync(username);

            if (admin == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(admin);
        }

        [HttpPut("admin-info")]
        public async Task<IActionResult> UpdateAdminInfo([FromBody] UpdateUserInfo model)
        {
            var admin = await userManager.FindByNameAsync(model.Username);

            if (admin == null)
            {
                return NotFound(new { message = "User not found." });
            }

            //admin.Email = model.Email;
            admin.UserName = model.Username;
            admin.PhoneNumber = model.PhoneNumber;
            admin.UserType = model.UserType;
            admin.Departement=model.Departement;

            var result = await userManager.UpdateAsync(admin);

            if (result.Succeeded)
            {
                return Ok(new { message = "Admin info updated successfully." });
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("change-admin-password")]
        public async Task<IActionResult> changeAdminPassword([FromBody] ChangePassword model)
        {
            var admin = await userManager.FindByNameAsync(model.Username);

            if (admin == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var result = await userManager.ChangePasswordAsync(admin, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new { message = "User password updated successfully" });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> AdminResetPassword([FromBody] ResetToDefaultDto model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
                return BadRequest("User not found.");

            await userManager.RemovePasswordAsync(user);
            await userManager.AddPasswordAsync(user,model.NewPassword);

            return Ok(new { Message = "Admin reset password to new password." });
        }


    }
}
