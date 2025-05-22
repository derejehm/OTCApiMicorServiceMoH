using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoH_Microservice.Data;
using MoH_Microservice.Misc;
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
        public readonly TokenValidate _tokenValidate;


        public OrganiztionController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext organiztion)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._organiztion = organiztion;
            this._tokenValidate = new TokenValidate(userManager);
        }
        [HttpGet("Organization")]
        public async Task<IActionResult> GetOrganiztions([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var organiztion = await this._organiztion.Set<Organiztion>().ToArrayAsync();
                return Created("/",organiztion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: fetching organiztion failed! Reason: {ex.Message}" });
            }
            

            
        }
        [HttpPost("Organization")]
        public async Task<IActionResult> AddOrganiztions([FromBody] OrganiztionReg organiztion, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                Organiztion recored = new Organiztion
                            {
                                Organization = organiztion.Organization,
                                Location=organiztion.Address,
                                CreatedBy = user.UserName,
                                CreatedOn = DateTime.Now,
                                UpdatedBy = null, UpdatedOn = null,
                            };

                await this._organiztion.AddAsync<Organiztion>(recored);
                await this._organiztion.SaveChangesAsync();
                return Ok(organiztion);
            }catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: Insert organizations failed! Reason: {ex.Message}" });
            }
            
        }

        [HttpPut("Organization")]
        public async Task<IActionResult> UpdateOrganiztions([FromBody] OrganiztionUpdate organiztion, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var updateOrg = await this._organiztion.Set<Organiztion>()
                                            .Where<Organiztion>((type) => type.Id == organiztion.Id)
                                             .ExecuteUpdateAsync(e =>
                                                        e.SetProperty(e => e.Organization, organiztion.Organization)
                                                        .SetProperty(e=>e.Location, organiztion.Address)
                                                        .SetProperty(e => e.UpdatedOn, DateTime.Now)
                                                        .SetProperty(e => e.UpdatedBy, user.UserName));

               return Ok($"Update - Organization updated to {organiztion.Organization}");
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: update organization failed! Reason: {ex.Message}" });
            }
            
        }

        [HttpDelete("Organization")]
        public async Task<IActionResult> DeleteOrganiztions([FromBody] OrganiztionDelete organiztion, [FromHeader] string Authorization)
        {
            try
            {

                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var updateOrg = await this._organiztion.Set<Organiztion>()
                                .Where<Organiztion>((type) => type.Id == organiztion.Id)
                                 .ExecuteDeleteAsync();

                return Ok($"Delete - item deleted");
            }
            catch(Exception ex)
            {
                return BadRequest(new { msg = $"Error: Delete Organization failed! Reason: {ex.Message}" });
            }

        }

        [HttpPost ("add-workers")]
        public async Task<IActionResult> AddWorking([FromBody] OrganiztionalUsersReg workers, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                if (workers.IsExtend)
                {
                    workers = await this.UniqueWorkers(user.Hospital.ToLower(),workers);
                }
                else
                {
                   var deleteWorkers = await this._organiztion.OrganiztionalUsers.Where(e => e.WorkPlace.ToLower() == workers.Workplace.ToLower() && e.AssignedHospital.ToLower()==user.Hospital.ToLower()).ExecuteDeleteAsync();
                }
                for (var i=0;i < workers.EmployeeEmail.Count; i++)
                {
                    OrganiztionalUsers worker = new OrganiztionalUsers
                    {
                        EmployeeID = workers.EmployeeID[i],
                        EmployeeName = workers.EmployeeName[i],
                        EmployeeEmail = workers.EmployeeEmail[i],
                        EmployeePhone = workers.EmployeePhone[i],
                        UploadedBy = user.UserName,
                        UploadedOn = DateTime.Now,
                        UpdatedBy=null,
                        UpdatedOn=null,
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
                return BadRequest(new { msg = $"Error: Updloading credit users failed! Reason: {ex.Message}" });
            }
        }

        [HttpGet("get-workers")]
        public async Task<IActionResult> GetWorkersAll([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var workers = await this._organiztion.OrganiztionalUsers
                    .Where(e => e.AssignedHospital.ToLower() == user.Hospital.ToLower())
                    .OrderByDescending(e => e.UploadedOn)
                    .Take(1000)
                    .ToArrayAsync();

                if (workers.Length <= 0)
                     NoContent();

              return Ok(workers);

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: fetching creidt users failed! Reason: {ex.Message}" });
            }

            
        }

        [HttpGet("get-workers/{EmployeeID}")]
        public async Task<IActionResult> GetWorkers([FromRoute] string EmployeeID, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var workers = await this._organiztion.OrganiztionalUsers
                    .Where(e => e.EmployeeID.ToLower() == EmployeeID.ToLower() 
                           && e.AssignedHospital.ToLower() == user.Hospital.ToLower())
                    .ToArrayAsync();

                if (workers.Length <= 0)
                    NoContent();
                return Ok(workers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: fetch credit users failed! Reason: {ex.Message}" });
            }


        }

        [HttpPut("Update-workers")]
        public async Task<IActionResult> UpdateWorkers([FromBody] OrganiztionalUsersUpdate worker, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var workers = await this._organiztion.OrganiztionalUsers
                .Where(e => e.Id == worker.Id)
                .ExecuteUpdateAsync(item => 
                        item
                        .SetProperty(d => d.EmployeeID, worker.EmployeeID)
                        .SetProperty(d => d.EmployeeName, worker.EmployeeName)
                        .SetProperty(d => d.EmployeePhone, worker.EmployeePhone)
                        .SetProperty(d => d.EmployeeEmail, worker.EmployeeEmail)
                        .SetProperty(d => d.WorkPlace, worker.Workplace)
                        .SetProperty(d => d.UpdatedOn, DateTime.Now)
                        .SetProperty(d => d.UpdatedBy, user.UserName)
                 );
            
            return Ok(workers);
            }catch (Exception ex) {
                return BadRequest(new { msg = $"Error: Update credit user {worker.EmployeeID} failed! Reason: {ex.Message}" });
            }
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
