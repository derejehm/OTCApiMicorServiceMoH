using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoH_Microservice.Data;
using MoH_Microservice.Misc;
using MoH_Microservice.Lib.Impliment;
using MoH_Microservice.Models.Database;
using System.Net;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private AppDbContext _dbContext;
        private TokenValidate _tokenValidate;
        private readonly RequestImpli _requestimpli;
        public RequestController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._dbContext = dbContext;
            this._tokenValidate = new TokenValidate(userManager);
            this._requestimpli = new RequestImpli(dbContext);
        }

        [HttpGet("doctor/get-request")]
        public async Task<IActionResult> getRequest([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getRequest = await this._requestimpli.getDoctorRequest(user);
                return Ok(new AppSuccess
                {
                    data= new JsonResult(getRequest)
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
        [HttpPost("doctor/order-request")]
        public async Task<IActionResult> orderRequest([FromBody]Models.Form.DoctorRequest request,[FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getRequest = await this._requestimpli.orderDoctorRequest(user, request);
                return Ok(new AppSuccess
                {
                    data = new JsonResult(getRequest)
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
        [HttpPut("doctor/fail-request")]
        public async Task<IActionResult> failRequest([FromBody]long id,[FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getRequest = await this._requestimpli.failedDoctorRequest(user,id);
                return Ok(new AppSuccess
                {
                    data = new JsonResult(getRequest ? "Order status chaged into failed!" : ".")
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
        [HttpPut("doctor/pick-request")]
        public async Task<IActionResult> pickRequest([FromBody] long id, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getRequest = await this._requestimpli.pickDoctorRequest(user, id);
                return Ok(new AppSuccess
                {
                    data = new JsonResult(getRequest ? "Order has been accepted for proccess!" : ".")
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

        [HttpPut("doctor/process-request")]
        public async Task<IActionResult> processRequest([FromBody] long id, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getRequest = await this._requestimpli.proccessDoctorRequest(user, id);
                return Ok(new AppSuccess
                {
                    data = new JsonResult(getRequest ? "Order status changed to processed" : ".")
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

        [HttpPut("doctor/complete-request")]
        public async Task<IActionResult> completeRequest([FromBody] long id, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getRequest = await this._requestimpli.completeDoctorRequest(user, id);
                return Ok(new AppSuccess
                {
                    data = new JsonResult(getRequest ? "Order has been Completed!" : ".")
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

        [HttpPut("doctor/cancel-request")]
        public async Task<IActionResult> cancelRequest([FromBody] string id, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getRequest = await this._requestimpli.cancelDoctorRequest(user, id);
                return Ok(new AppSuccess
                {
                    data = new JsonResult(getRequest? "Order has been canceled!":".")
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
        [HttpPut("doctor/cancel-request/{id}")]
        public async Task<IActionResult> cancelRequest([FromRoute] long id, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getRequest = await this._requestimpli.cancelDoctorRequest(user, id);
                return Ok(new AppSuccess
                {
                    data = new JsonResult(getRequest ? "Order has been canceled!" : ".")
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
        [HttpGet("doctor/get-request-lab")]
        public async Task<IActionResult> getRequestLab([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getRequest = await this._requestimpli.getDoctorRequestLab_v();
                return Ok(new AppSuccess
                {
                    data = new JsonResult(getRequest)
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
        [HttpGet("doctor/get-request-casheir")]
        public async Task<IActionResult> getRequestCasheir([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getRequest = await this._requestimpli.getDoctorRequestCashier_v();
                return Ok(new AppSuccess
                {
                    data = new JsonResult(getRequest)
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
        [HttpGet("doctor/get-request-pharma")]
        public async Task<IActionResult> getRequestPharma([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getRequest = await this._requestimpli.getDoctorRequestPharma_v();
                return Ok(new AppSuccess
                {
                    data = new JsonResult(getRequest)
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
        [HttpGet("doctor/get-request-pharma/paid")]
        public async Task<IActionResult> getPaidRequestPharma([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getRequest = await this._requestimpli.getDoctorRequestPharma_paid();
                return Ok(new AppSuccess
                {
                    data = new JsonResult(getRequest)
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

        [HttpGet("doctor/get-request-lab/paid")]
        public async Task<IActionResult> getPaidRequestLab([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getRequest = await this._requestimpli.getDoctorRequestlab_paid();
                return Ok(new AppSuccess
                {
                    data = new JsonResult(getRequest)
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
