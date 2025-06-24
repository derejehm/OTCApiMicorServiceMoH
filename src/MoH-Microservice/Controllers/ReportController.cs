using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MoH_Microservice.Data;
using MoH_Microservice.Lib.Impliment;
using MoH_Microservice.Lib.Implimentation;
using MoH_Microservice.Misc;
using MoH_Microservice.Models.Database;
using MoH_Microservice.Models.Form;
using MoH_Microservice.Query;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _dbContext;
        private TokenValidate _tokenValidate;
        private readonly ILogger _logger;   
        private readonly ReportImpli _reportImpli;

        public ReportController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._dbContext = dbContext;
            this._tokenValidate = new TokenValidate(userManager);
            this._reportImpli = new ReportImpli(dbContext); 
        }
        [HttpGet("get-report")]
        public async Task<IActionResult> getAllReport([FromHeader] string Authorization)
        {
            // get a list of the reports
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var report = await this._reportImpli.getReport();
                return Ok( new AppSuccess { 
                    data = new JsonResult(report   )
                } );
            }
            catch(Exception ex)
            {
                return BadRequest(new AppError { 
                        ErrorCode=ex.GetHashCode(),
                        ErrorDescription=ex.Message
                });
            }
            
        }
        [HttpGet("get-report/{reportuuid}")]
        public async Task<IActionResult> getOneReport([FromRoute] string reportuuid, [FromHeader] string Authorization)
        {
            // get a single report using the uuid
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var report = await this._reportImpli.getReport(reportuuid);
                return Ok(new AppSuccess
                {
                    data = new JsonResult(report)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError
                {
                    
                    ErrorCode = ex.GetHashCode(),
                    ErrorDescription = ex.Message
                });
            }
        }

        [HttpPut("excute-report")]
        public async Task<IActionResult> excuteReport([FromBody] Models.Form.ReportExcute filters, [FromHeader] string Authorization)
        {
            // get a single report using the uuid
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var report = await this._reportImpli.excuteReport(user,filters);
                return Ok(new AppSuccess
                {
                    data = new JsonResult(report)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError
                {
                    ErrorCode = ex.GetHashCode(),
                    ErrorDescription = ex.Message
                });
            }
        }

        [HttpPost("create-report")]
        public async Task<IActionResult> createReport([FromBody] Models.Form.Report report, [FromHeader] string Authorization)
        {
            // generate the "Sql Command and store it in a database"
            
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var create = await this._reportImpli.createReport(user, report);
                return Ok(new AppSuccess { data = new JsonResult(create) });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError
                {
                    ErrorCode = ex.GetHashCode(),
                    ErrorDescription = ex.Message
                });
            }
        }

        [HttpPost("add-source")]
        public async Task<IActionResult> sourceReport([FromBody] Models.Form.ReportSource report, [FromHeader] string Authorization)
        {
            // generate the "Sql Command and store it in a database"

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var create = await this._reportImpli.sourceReport(user, report);
                return Ok(new AppSuccess { data = new JsonResult(create) });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError
                {
                    ErrorCode = ex.GetHashCode(),
                    ErrorDescription = ex.Message
                });
            }
        }
        [HttpGet("get-source")]
        public async Task<IActionResult> getsourceReport([FromHeader] string Authorization)
        {
            // generate the "Sql Command and store it in a database"

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var create = await this._reportImpli.getSource();
                return Ok(new AppSuccess { data = new JsonResult(create) });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError
                {
                    ErrorCode = ex.GetHashCode(),
                    ErrorDescription = ex.Message
                });
            }
        }

        [HttpPut("update-report/{reportuuid}")]
        public async Task<IActionResult> updateReport([FromRoute]string reportuuid, [FromBody] Models.Form.Report report, [FromHeader] string Authorization)
        {
            // updateReport using the report uuid
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var update = await this._reportImpli.updateReport(user, reportuuid, report);
                return Ok(new AppSuccess {data = new JsonResult(update)});
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError
                {
                    
                    ErrorCode = ex.GetHashCode(),
                    ErrorDescription = ex.Message
                });
            }
        }

        [HttpDelete("remove-report/{reportuuid}")]
        public async Task<IActionResult> deleteReport([FromRoute] string reportuuid, [FromHeader] string Authorization)
        {
            // remove the report using the uuid
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var remove = await this._reportImpli.removeReport(reportuuid);
                return Ok(new AppSuccess
                {
                   data = new JsonResult(remove)
                });;
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError
                {
                   
                    ErrorCode = ex.GetHashCode(),
                    ErrorDescription = ex.Message
                });
            }
        }

        [HttpPut("save-report/{reportuuid}")]
        public async Task<IActionResult> saveReport([FromRoute] string reportuuid, [FromHeader] string Authorization)
        {
            // save the reports "command" with the "where condition"
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var save = await this._reportImpli.saveReport(user, reportuuid);
                return Ok(new AppSuccess
                { 
                    data = new JsonResult(save)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError
                {
                   
                    ErrorCode = ex.GetHashCode(),
                    ErrorDescription = ex.Message
                });
            }
        }

        [HttpPut("grunt-report")]
        public async Task<IActionResult> gruntReport( [FromBody] Models.Form.ReportAccess reportAccess, [FromHeader] string Authorization)
        {
            // save the reports "command" with the "where condition"
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var grunt = await this._reportImpli.enableAccessReport(user, reportAccess);
                return Ok(new AppSuccess
                {
                    data = new JsonResult(grunt)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError
                {
                    
                    ErrorCode = ex.GetHashCode(),
                    ErrorDescription = ex.Message
                });
            }
        }

        [HttpPut("enable-report/{reportuuid}")]
        public async Task<IActionResult> enableReport([FromRoute] string reportuuid, [FromHeader] string Authorization)
        {
            // save the reports "command" with the "where condition"
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var grunt = await this._reportImpli.enableAccessReport(reportuuid);
                return Ok(new AppSuccess
                {
                    data = new JsonResult(grunt)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError
                {
                    ErrorCode = ex.GetHashCode(),
                    ErrorDescription = ex.Message
                });
            }
        }
    }
}
