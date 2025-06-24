using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoH_Microservice.Data;
using MoH_Microservice.Misc;
using MoH_Microservice.Models.Form;
using static MoH_Microservice.Misc.AppReportModel;
using MoH_Microservice.Query;
using MoH_Microservice.Lib.Impliment;
using MoH_Microservice.Models.Database;
using Microsoft.DotNet.Scaffolding.Shared;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private AppDbContext _dbContext;
        private TokenValidate _tokenValidate;
        public AppQuery _appQuery;
        public PatientImpli _patientImpli;
        //public RequestImpli _patientImpli;

        public PatientController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,AppDbContext payment)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._dbContext = payment;
            this._tokenValidate = new TokenValidate(userManager);
            this._appQuery = new AppQuery(this._dbContext);
            this._patientImpli = new PatientImpli(this._dbContext);
        }

        [HttpPost("add-patient-info")]
        public async Task<IActionResult> addGetPatientInfo([FromBody] PatientReg patient, [FromHeader] string Authorization)
        {
            try
            { 
               var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
               var _patient = await this._patientImpli.addPatient(user,patient);
               return Created("/", new AppSuccess
                {

                    data = new JsonResult(_patient)

                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }

        [HttpPut("update-patient-info")]
        public async Task<IActionResult> ChangePatientInfo([FromBody] PatientUpdate patient, [FromHeader] string Authorization)
        {

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var _patient = await this._patientImpli.modifyPatient(user, patient);
                return Ok(new AppSuccess
                {
                    
                    data = new JsonResult(_patient)

                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }
        [HttpPut("get-patient-info")]
        public async Task<IActionResult> GetPatientInfo([FromBody] PatientView patient, [FromHeader] string Authorization)
        {

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var SearchResult = await this._patientImpli.findPatient(patient);
                var patientInfo = await this._patientImpli.findPatientCount();

                return Ok(new AppSuccess
                {
                    data = new JsonResult(SearchResult),
                    count = patientInfo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Message,ErrorException=ex.InnerException });
            }
        }
        [HttpPut("get-one-patient-info")]
        public async Task<IActionResult> GetOnePatientInfo([FromBody] PatientViewGetOne patient, [FromHeader] string Authorization)
        {
            try
            {
                DateTime start = DateTime.Now;
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var patientInfo = await this._patientImpli.getOnePatient(patient.PatientCardNumber);
                return Ok(new AppSuccess
                {
                   
                    data = new JsonResult(patientInfo),
                    elapsedTime = (DateTime.Now - start)

                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError {  ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }


        [HttpPost("add-patient-cbhi")]
        public async Task<IActionResult> addCBHIPatient([FromBody] ProvidersMapReg providers, [FromHeader] string Authorization)
        {
            try
            {
                DateTime start = DateTime.Now;
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var provider = await this._patientImpli.addPatientCBHI(user, providers);

                return Created("/", new AppSuccess
                {
                    
                    data = new JsonResult(provider),
                    elapsedTime = (DateTime.Now - start)

                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError
                {
                    
                    ErrorCode =ex.GetHashCode(),
                    ErrorDescription = ex.Message,
                });
            }
        }

        [HttpPost("get-patient-cbhi")]
        public async Task <IActionResult> getCBHIPatient([FromHeader] string Authorization)
        {
            try
            {
                DateTime start = DateTime.Now;
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var provider = await this._patientImpli.getAllPatientCBHI();
                var result = provider.Take(10000);

                return Ok(new AppSuccess
                {
               
                    data = new JsonResult(result),
                    elapsedTime = (DateTime.Now - start)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError
                {
                    
                    ErrorCode = ex.GetHashCode(),
                    ErrorDescription = ex.Message,
                });
            }
        }

        [HttpPost("add-patient-request")]
        public async Task<IActionResult> addPatientRequestInfo([FromBody] PatientRequestedServicesReg PatientRequest, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var requesedLabServices = await this._patientImpli.addLabRequest(user, PatientRequest);

                return Ok(new { data = PatientRequest, groupid = requesedLabServices });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError
                {
                   
                    ErrorCode = ex.GetHashCode(),
                    ErrorDescription = ex.Source,
                });
            }
        }

        [HttpPut("get-patient-request")]
        public async Task<IActionResult> getOnePatientRequestInfo([FromBody] PatientRequestedServicesViewOne patient, [FromHeader] string Authorization)
        {
            try
            {
                // check the user form token and verify its eistnce in db
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getLabRequest = await this._patientImpli.getLabRequest(patient);
                
                if (!getLabRequest.Any())
                    return NoContent();

                return Ok(getLabRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }

        [HttpPut("get-patient-request-cashier")]
        public async Task<IActionResult> getPatientRequestInfoCashier([FromBody] PatientRequestedServicesViewOne patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var getLabRequest = await this._patientImpli.getLabRequest(user, patient);

                if (!getLabRequest.Any())
                    return NoContent();
                return Ok(getLabRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }

        [HttpPut("complete-patient-request")]
        public async Task<IActionResult> updatePatientRequestInfo([FromBody] PatientRequestedServicesViewOne patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var completeLabRequest = await this._patientImpli.completeLabRequest(user,patient);
                if(completeLabRequest)
                    return Ok(completeLabRequest?"Lab Request has been completed":"");
                throw new Exception("Unable to complete lab request.");
            }
            catch(Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }
        [HttpDelete("cancel-patient-request")]
        public async Task<IActionResult> deletePatientRequestInfo([FromBody] PatientRequestedServicesDelete patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var cancelLabRequest = await this._patientImpli.cancelLabRequest(patient);
                if (cancelLabRequest)
                    return Ok(cancelLabRequest?"Lab Request has been canceled":"");
                throw new Exception("Unable to cancel lab request.");
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }
        //
        [HttpPost("add-nurse-request")]
        public async Task<IActionResult> addNurseRequestInfo([FromBody] NurseRequestReg nurseRequest, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var addNurseRequest = this._patientImpli.addNurseRequest(user, nurseRequest);

                return Ok(new { data = nurseRequest, groupid = addNurseRequest });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }

        [HttpPut("get-nurse-request")]
        public async Task<IActionResult> getOneNurseRequestInfo([FromBody] NurseRequestGetOne patient, [FromHeader] string Authorization)
        {
            try
            {
                // check the user form token and verify its eistnce in db
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var result = await this._patientImpli.getNurseRequest(patient);

                if (!result.Any())
                    return NoContent();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }

        [HttpPut("get-nurse-request-cashier")]
        public async Task<IActionResult> getNurseRequestInfoCashier([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var result = await this._patientImpli.getNurseRequest(user);

                if (!result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }
        [HttpDelete("cancel-nurse-request")]
        public async Task<IActionResult> deleteNurseRequestInfo([FromBody] PatientRequestedServicesDelete patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var cancelNurseRequest = await this._patientImpli.cancelNurseRequest(patient);

                return Ok(cancelNurseRequest? "Nurses Request has been canceled":"");  
                
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }
        [HttpPut("complete-nurse-request")]
        public async Task<IActionResult> updateNurseRequestInfo([FromBody] PatientRequestedServicesViewOne patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var completeNurseRequest = await this._patientImpli.completeNurseRequest(user, patient);

                return Ok(completeNurseRequest ? "Nurses Request has been completed" : "");

            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }
        [HttpPost("add-patient-accedent")]
        public async Task<IActionResult> addPatientAccedent([FromBody] PatientAccedentsReg patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var doesPatientExisit = await this._patientImpli.addPatientAccedent(user, patient);
                
                return Ok(doesPatientExisit);
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }

        [HttpPut("get-patient-accedent")]
        public async Task<IActionResult> getPatientAccedent([FromBody] PatientView patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var Output = this._patientImpli.getAllPatientAccedent(patient);
                return Ok(new { data= Output });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }

        [HttpPut("change-patient-accedent")]
        public async Task<IActionResult> UpdatePatientAccedent([FromBody] PatientAccedentsReg patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var modifiedAccedentInformation =await this._patientImpli.modifyAccedentPatient(user, patient);
                return Ok(new { msg = modifiedAccedentInformation ? "ACCEDENT CHANGED SUCCESSFULY!" : "" });
            }
            catch (Exception ex)
            {
                return BadRequest(new AppError { ErrorCode = ex.GetHashCode(), ErrorDescription = ex.Source });
            }
        }  
    }
}
