using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
                Location=organiztion.Address,
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
                                        .SetProperty(e=>e.Location, organiztion.Address)
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
            if (workers.IsExtend)
            {
                workers = await this.UniqueWorkers(user.Hospital.ToLower(),workers);
            }
            else
            {
               var deleteWorkers = await this._organiztion.OrganiztionalUsers.Where(e => e.WorkPlace.ToLower() == workers.Workplace.ToLower() && e.AssignedHospital.ToLower()==user.Hospital.ToLower()).ExecuteDeleteAsync();
            }
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
                        WorkPlace = workers.Workplace,

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

        [HttpGet("get-workers/{LoggedInUser}")]
        public async Task<IActionResult> GetWorkersAll([FromRoute] OrganizationalUserGet worker)
        {
            var user = await this._userManager.FindByNameAsync(worker.LoggedInUser);
            if (user == null)
                return NotFound("User not found");
            var workers = await this._organiztion.OrganiztionalUsers
                .Where(e=> e.AssignedHospital.ToLower() == user.Hospital.ToLower())
                .OrderByDescending(e=>e.UploadedOn)
                .Take(1000)
                .ToArrayAsync();
            if (workers.Length <= 0)
                 NoContent();
          return Ok(workers);
            

        }

        [HttpGet("get-workers/{LoggedInUser}/{EmployeeID}")]
        public async Task<IActionResult> GetWorkers([FromRoute] OrganizationalUserGet worker)
        {
            var user = await this._userManager.FindByNameAsync(worker.LoggedInUser);
            if (user == null)
                return NotFound("User not found");

            var workers = await this._organiztion.OrganiztionalUsers
                .Where(e => e.EmployeeID.ToLower() == worker.EmployeeID.ToLower() && e.AssignedHospital.ToLower() == user.Hospital.ToLower())
                .ToArrayAsync();
            if (workers.Length <=0)
                NoContent();
            return Ok(workers);

        }

        private async Task<OrganiztionalUsersReg> UniqueWorkers(string hospital,OrganiztionalUsersReg workers)
        {
            List<string> _EmployeeID = new List<string>();
            List<string> _EmployeeName = new List<string>();
            List<string> _EmployeeEmail = new List<string>();
            List<string> _EmployeePhone = new List<string>();
            for (var i = 0; i < workers.EmployeeEmail.Count; i++)
            {
                var employee = await this._organiztion.OrganiztionalUsers
                    .Where(e => e.EmployeeID.ToLower() == workers.EmployeeID[i].ToLower() 
                    && e.WorkPlace.ToLower() == workers.Workplace.ToLower() 
                    && e.AssignedHospital.ToLower() == hospital)
                    .ToArrayAsync();
                if (employee.Length <= 0)
                {
                    _EmployeeID.Add(workers.EmployeeID[i]);
                    _EmployeeName.Add(workers.EmployeeName[i]);
                    _EmployeeEmail.Add(workers.EmployeeEmail[i]);
                    _EmployeePhone.Add(workers.EmployeePhone[i]);
                }
            }
            OrganiztionalUsersReg worker = new OrganiztionalUsersReg
            {
                EmployeeID =  _EmployeeID,
                EmployeeName = _EmployeeName,
                EmployeeEmail = _EmployeeEmail,
                EmployeePhone =_EmployeePhone,
                UploadedBy = workers.UploadedBy,
                Workplace = workers.Workplace,
            };

            return worker;
        }

    }
}
