using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoH_Microservice.Data;
//using MoH_Microservice.Migrations;
using MoH_Microservice.Models;
using NuGet.Protocol;
using NuGet.Versioning;
using System.Linq;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LookupController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private AppDbContext _payment;
        public LookupController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext payment
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._payment = payment;
        }

        [HttpGet("payment-verify/{receptId}")]
        public async Task<IActionResult> PaymentVerify([FromRoute] string receptId)
        {

            var BankQrLinkList = new List<BankLinkList>();
            BankQrLinkList.Add(new BankLinkList { Institution = "telebirr", QRLink = $"https://transactioninfo.ethiotelecom.et/receipt/{receptId}" });
            BankQrLinkList.Add(new BankLinkList { Institution = "cbe", QRLink = $"https://apps.cbe.com.et:100/?id={receptId}" });
            // Browse and Return a HTML Page

            return Created($"/{receptId}", new JsonResult(BankQrLinkList).Value);
        }
        [HttpGet("payment-info")]
        [Authorize(Policy = "AdminPolicy")] // 
        public async Task<IActionResult> GetAllPaymentInfo([FromBody] string department)
        {
            if (department != "Tsedey Bank") return NoContent();

            var PymentInfo = await this._payment.Set<Payment>()
                            .GroupBy((col) => new { Hospital = col.HospitalName, Casher = col.Createdby, Type = col.Type, Purpose = col.Purpose })
                            .Select((select) =>
                                new {
                                    Hospital = select.Key.Hospital,
                                    Casher = select.Key.Casher,
                                    Type = select.Key.Type,
                                    Purpose = select.Key.Purpose,
                                    Amount = select.Sum((payment) => payment.Amount)
                                }
                            ).ToArrayAsync();

            if (PymentInfo.Length <= 0)
                return NoContent();

            return Ok(new JsonResult(PymentInfo).Value);
        }

        [HttpGet("payment-channel")]
        public async Task<IActionResult> GetAllPaymentChannel()
        {
            var PymentInfo = await this._payment.Set<PaymentChannel>().ToArrayAsync();

            if (PymentInfo.Length <= 0)
                return NoContent();

            return Ok(new JsonResult(PymentInfo).Value);
        }
        [HttpGet("payment-type")]
        public async Task<IActionResult> GetAllPaymentType()
        {
            var PymentInfo = await this._payment.Set<PaymentType>().ToArrayAsync();

            if (PymentInfo.Length <= 0)
                return NoContent();

            return Ok(new JsonResult(PymentInfo).Value);
        }

        [HttpGet("payment-purpose")]
        public async Task<IActionResult> GetAllPaymentPurpose()
        {
            var PymentInfo = await this._payment.Set<PaymentPurpose>().ToArrayAsync();

            if (PymentInfo.Length <= 0)
                return NoContent();

            return Ok(new JsonResult(PymentInfo).Value);
        }

        [HttpPost("add-paymentType")]
        // admin / supervisors
        public async Task<IActionResult> SetPaymentType([FromBody] PaymentTypeReg paymentType)
        {
            var username = await this._userManager.FindByNameAsync(paymentType.CreatedBy); // Check if the user exists
            if (username == null)
                return NotFound("User not found");
            var PymentInfo = await this._payment.Set<PaymentType>().Where<PaymentType>((type) => type.type == paymentType.type).ToArrayAsync();

            if (PymentInfo.Length > 0)
                return BadRequest("Payment type aleady exist");

            PaymentType type = new PaymentType
            {
                type = paymentType.type,
                CreatedBy = paymentType.CreatedBy,
                CreatedOn = DateTime.Now
            };

            await this._payment.AddAsync<PaymentType>(type);
            await this._payment.SaveChangesAsync();

            return Created("/", new JsonResult(type).Value);
        }

        [HttpPost("add-paymentChannel")]
        // admin / supervisors
        public async Task<IActionResult> SetPaymentChannels([FromBody] PaymentChannelReg paymentChannel)
        {
            var username = await this._userManager.FindByNameAsync(paymentChannel.CreatedBy); // Check if the user exists
            if (username == null)
                return NotFound("User not found");

            var PymentInfo = await this._payment.Set<PaymentChannel>().Where<PaymentChannel>((type) => type.Channel == paymentChannel.Channel).ToArrayAsync();

            if (PymentInfo.Length > 0)
                return BadRequest("Payment channel aleady exist");

            PaymentChannel Channel = new PaymentChannel
            {
                Channel = paymentChannel.Channel,
                CreatedBy = paymentChannel.CreatedBy,
                CreatedOn = DateTime.Now
            };

            await this._payment.AddAsync<PaymentChannel>(Channel);
            await this._payment.SaveChangesAsync();

            return Created("/", new JsonResult(Channel).Value);
        }

        [HttpPost("add-paymentPurpose")]
        // admin / supervisors
        public async Task<IActionResult> SetPaymentPurpose([FromBody] PaymentPurposeReg paymentPurpose)
        {
            var username = await this._userManager.FindByNameAsync(paymentPurpose.CreatedBy); // Check if the user exists
            if (username == null)
                return NotFound("User not found");

            var PymentInfo = await this._payment.Set<PaymentPurpose>().Where<PaymentPurpose>((e) => e.Purpose == paymentPurpose.Purpose).ToArrayAsync();

            if (PymentInfo.Length > 0)
                return BadRequest("Payment purpose aleady exist");

            PaymentPurpose purpose = new PaymentPurpose
            {
                Purpose = paymentPurpose.Purpose,
                CreatedBy = paymentPurpose.CreatedBy,
                CreatedOn = DateTime.Now
            };

            await this._payment.AddAsync<PaymentPurpose>(purpose);
            await this._payment.SaveChangesAsync();

            return Created("/", new JsonResult(purpose).Value);
        }

        private class BankLinkList
        {

            public string? Institution { get; set; }
            public string? QRLink { get; set; }
        }

    }
}
