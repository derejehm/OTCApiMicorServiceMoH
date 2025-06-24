
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoH_Microservice.Data;
using MoH_Microservice.Misc;
using MoH_Microservice.Query;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using MoH_Microservice.Lib.Implimentation;
using MoH_Microservice.Models.Form;
using MoH_Microservice.Models.Database;


namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _payment;
        private readonly AppQuery _query;
        private TokenValidate _tokenValidate;
        private readonly PaymentImpli _paymentImpli;

        public PaymentController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,AppDbContext payment) 
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._payment = payment;
            this._query = new AppQuery(payment);
            this._tokenValidate = new TokenValidate(userManager);
            this._paymentImpli = new PaymentImpli(payment,this._query);
        }

        [HttpPut("Get-all-Payment")]
        public async Task<IActionResult> GetPaymentByDate([FromBody] PaymentbyDate payment, [FromHeader] string Authorization)
        {
            
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var data = await this._paymentImpli.getAllPayment(user, payment);
                if (data.Length <= 0)
                    return NoContent();
                return Ok(new JsonResult(data).Value);

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            
        }
        [HttpPut("rpt-all-Payment")] // report excluding accedents treatments
        public async Task<IActionResult> RptPaymentByDate([FromBody] PaymentbyDate payment, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var data = await this._paymentImpli.getAllRptPayment(user, payment);
                if (data.Length <= 0)
                    return NoContent();
                return Ok(new JsonResult(data).Value);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            } 
        }

        [HttpPut("payment-by-refno")]
        public async Task<IActionResult> GetPaymentInfoByRefNo([FromBody] PaymentInfo payment, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                AppReportModel.PaymentReportDTO[] PymentInfo = [];
                if (payment.Isreversed != null) {
                     PymentInfo = await this._paymentImpli.getPaymentByRefereceNumber(payment.paymentId, payment.Isreversed);
                }else {
                     PymentInfo = await this._paymentImpli.getPaymentByRefereceNumber(payment.paymentId);
                }
                 
                if (!PymentInfo.Any())
                    return NoContent();

                return Ok(new JsonResult(PymentInfo).Value);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            
        }

        [HttpPut("payment-by-RefNoInstitution")]
        public async Task<IActionResult> GetPaymentInfoByRefNo([FromBody] PaymentDetailByInstitution payment, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._query.PaymentQuery()
                    .Where(x => x.ReferenceNo == payment.paymentId && x.HospitalName == payment.hospital)
                    .ToArrayAsync();

                if (PymentInfo.Length <= 0)
                    return NoContent();
                return Ok(new JsonResult(PymentInfo).Value);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            
        }
        [HttpPut("payment-by-cashier")]

        public async Task<IActionResult> GetPaymentInfoByCashier([FromHeader] string Authorization)
        { 
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._paymentImpli.getPaymentByCashier(user.UserName);
                if (PymentInfo.Length <= 0)
                    return NoContent();

                return Ok(new JsonResult(PymentInfo).Value);
            }
            catch(Exception ex)
            {
                   return BadRequest(ex.Message);
            }  
        }

        [HttpPut("payment-by-cardNumber")]
        public async Task<IActionResult> GetPaymentInfoByCardNumber([FromBody] PaymentDetailByCardNo payment, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._paymentImpli.getPaymentByPatientCardNumber(payment.PatientCardNumber);
                if (PymentInfo.Length <= 0)
                    return NoContent();

                return Ok(new JsonResult(PymentInfo).Value);
            }catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpPut("payment-by-phonenumber")]
        public async Task<IActionResult> getPaymentByPphone([FromBody] PaymentDetailByPhone payment, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._paymentImpli.getPaymentByPhonenumber(payment.phone);

                if (PymentInfo.Length <= 0)
                    return NoContent();

                return Ok(new JsonResult(PymentInfo).Value);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }

        }
        [HttpPut("payment-by-patientname")]
        public async Task<IActionResult> getPaymentByPname([FromBody] PaymentDetailByName payment, [FromHeader] string Authorization)
        {

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._paymentImpli.getPaymentByName(payment.patient);
                if (PymentInfo.Length <= 0)
                    return NoContent();

                return Ok(new JsonResult(PymentInfo).Value);
            }
            catch(Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
        
        [HttpPost("add-payment")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> InsertPaymentInfo([FromBody] PaymentReg payment,[FromHeader] string Authorization)
        {
            //using (var transaction = await _payment.Database.BeginTransactionAsync())
            //{
                try
                {
                    var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                    List<PatientReuestServicesViewDTO[]> groupPayment = new List<PatientReuestServicesViewDTO[]>();
                    if (user.UserType.ToLower() != "cashier")
                        throw new Exception("YOU CAN'T PERFORM PAYMENT");

                    if (!payment.PaymentRefNo.IsNullOrEmpty() && payment.reverse == true)
                    {
                        var reverse = await this._paymentImpli.cancelPayment(user, payment);
                       // await transaction.CommitAsync();
                        return Ok(reverse);
                    }

                    var paymentDetails = await this._paymentImpli.addPayment(user, payment, groupPayment);
                   // await transaction.CommitAsync();
                    return Created("/", new
                    {
                        RefNo = paymentDetails?.FirstOrDefault()?.ReferenceNo,
                        data = paymentDetails,
                        grp_exisiting_payment = groupPayment
                    });
                }
                catch (Exception ex)
                {
                   // await transaction.RollbackAsync();
                    return BadRequest(new AppError { ErrorDescription = $"{ex.Message}" });
                }
            //}
     
        }

        [HttpGet("get-payment-bytype")]
        public async Task<IActionResult> GetAllProviderInfo([FromRoute] string paymentType,[FromHeader] string Authorization)
        {

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var patientInfo = await this._paymentImpli.getPaymentByType(paymentType);

                if (!patientInfo.Any())
                {
                    return NoContent();
                }

                return Ok(patientInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        private class BankLinkList
        {

            public string? Institution { get; set; }
            public string? QRLink { get; set; }
        }
    }
}
