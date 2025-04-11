using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoH_Microservice.Data;
using MoH_Microservice.Models;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganiztionController : ControllerBase
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly AppDbContext _organiztion;


        public OrganiztionController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext organiztion)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._organiztion = organiztion;
        }
        [HttpGet("Organization/{loggedInUser}")]
        public async Task<IActionResult> GetOrganiztions([FromRoute] string loggedInUser)
        {
            var user = await this._userManager.FindByNameAsync(loggedInUser);
            if (user == null)
                return NotFound("User not found");

            var organiztion = await this._organiztion.Set<Organiztion>().ToArrayAsync();

            return Created("/",organiztion);
        }
        [HttpPost("Organization")]
        public async Task<IActionResult> AddOrganiztions([FromBody] OrganiztionReg organiztion)
        {
            var user = await this._userManager.FindByNameAsync(organiztion.CreatedBy);
            if (user == null)
                return NotFound("User not found");

            Organiztion recored = new Organiztion
            {
                Organization = organiztion.Organization,
                CreatedBy = organiztion.CreatedBy,
                CreatedOn = DateTime.Now,
                UpdatedBy = null, UpdatedOn = null,
            };

            await this._organiztion.AddAsync<Organiztion>(recored);
            await this._organiztion.SaveChangesAsync();

            return Ok(organiztion);
        }

        [HttpPut("Organization")]
        public async Task<IActionResult> UpdateOrganiztions([FromBody] OrganiztionUpdate organiztion)
        {
            var user = await this._userManager.FindByNameAsync(organiztion.UpdatedBy);
            if (user == null)
                return NotFound("User not found");

            var updateOrg = await this._organiztion.Set<Organiztion>()
                            .Where<Organiztion>((type) => type.Id == organiztion.Id)
                             .ExecuteUpdateAsync(e =>
                                        e.SetProperty(e => e.Organization, organiztion.Organization)
                                        .SetProperty(e => e.UpdatedOn, DateTime.Now)
                                        .SetProperty(e => e.UpdatedBy, organiztion.UpdatedBy));

            return Ok($"Update - Organization updated to {organiztion.Organization}");
        }

        [HttpDelete("Organization")]
        public async Task<IActionResult> DeleteOrganiztions([FromBody] OrganiztionDelete organiztion)
        {
            var user = await this._userManager.FindByNameAsync(organiztion.deletedBy);
            if (user == null)
                return NotFound("User not found");

            var updateOrg = await this._organiztion.Set<Organiztion>()
                            .Where<Organiztion>((type) => type.Id == organiztion.Id)
                             .ExecuteDeleteAsync();

            return Ok($"Delete - item deleted");
        }

        [HttpPost ("add-workers")]
        public async Task<IActionResult> AddWorking([FromBody] OrganiztionalUsersReg workers)
        {
            var user = await this._userManager.FindByNameAsync(workers.UploadedBy);
            if (user == null)
                return NotFound("User not found");
            try
            {
                for (var i=0;i < workers.EmployeeEmail.Count; i++)
                {
                    OrganiztionalUsers worker = new OrganiztionalUsers
                    {
                        EmployeeID = workers.EmployeeID[i],
                        EmployeeName = workers.EmployeeName[i],
                        EmployeeEmail = workers.EmployeeEmail[i],
                        EmployeePhone = workers.EmployeePhone[i],
                        UploadedBy = workers.UploadedBy,
                        UploadedOn = DateTime.Now,
                        AssignedHospital = user.Hospital,

                    };

                    await this._organiztion.OrganiztionalUsers.AddAsync(worker);
                    await this._organiztion.SaveChangesAsync();
                }

                return Ok("Data uploaded Successfully!");
                
            }
            catch (Exception ex)
            {
                BadRequest($"Insertion failed reason : {ex.Message}");
            }

            return Ok("Data uploaded Successfully!");
        }

        [HttpGet("get-workers/{loggedInUser}")]
        public async Task<IActionResult> GetWorkers([FromRoute] string loggedInUser)
        {
            var user = await this._userManager.FindByNameAsync(loggedInUser);
            if (user == null)
                return NotFound("User not found");
            var workers = await this._organiztion.OrganiztionalUsers.ToArrayAsync();
            if (workers == null)
                NoContent();
            return Ok(workers);
        }

    }
}
