
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MoH_Microservice.Data;
using MoH_Microservice.Misc;
using MoH_Microservice.Models;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Text.Json;

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
        private TokenValidate _tokenvalidate;
        

        public LookupController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext payment
          
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._payment = payment;
            this._tokenvalidate = new TokenValidate(userManager);


        }



        [HttpGet("payment-verify/{receptId}")]
        public async Task<IActionResult> PaymentVerify(string receptId,string channel)
        {

            HttpClient client = new HttpClient();
            var url = "";
            if (channel.ToUpper() == "TELEBIRR")
            {
                url = $"https://transactioninfo.ethiotelecom.et/receipt/{receptId}";
            }
            
            if(channel.ToUpper() == "CBE MOBILE BANKING")
            {
                url = $"https://apps.cbe.com.et:100/?id={receptId}";
            }

            if (channel.ToUpper() == "BANK OF ABYSSINIA")
            {
                url = $"https://cs.bankofabyssinia.com/api/onlineSlip/getDetails/?id={receptId}";

                //https://cs.bankofabyssinia.com/api/onlineSlip/getDetails/?id=FT2509091DCW10104
            }


            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
              
                if (channel.ToUpper() == "TELEBIRR")
                {
                    return Ok(await response.Content.ReadAsStringAsync());
                }

                if (channel.ToUpper() == "CBE MOBILE BANKING") 
                {

                    return File(await response.Content.ReadAsByteArrayAsync(), "application/pdf", "payment_verification.pdf");
                    //return Ok(await response.Content.ReadAsByteArrayAsync());
                }

                if (channel.ToUpper() == "BANK OF ABYSSINIA")
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                    return Ok(result);
                    //return Ok(await response.Content.ReadAsByteArrayAsync());
                }




            }
            else
            {
               // throw new HttpRequestException("Erorr response: " + response.ReasonPhrase);

                return BadRequest(new {message= "Incorrect referance number" });
            }

            // return BadRequest();

            //var BankQrLinkList = new List<BankLinkList>();
            //BankQrLinkList.Add(new BankLinkList { Institution = "telebirr", QRLink = $"https://transactioninfo.ethiotelecom.et/receipt/{receptId}" });
            //BankQrLinkList.Add(new BankLinkList { Institution = "cbe", QRLink = $"https://apps.cbe.com.et:100/?id={receptId}" });

            // Browse and Return a HTML Page

            //return Created($"/{receptId}", new JsonResult(BankQrLinkList).Value);

            return BadRequest(new { message = "Incorrect referance number" });
        }
        [HttpGet("payment-info")]
        [Authorize(Policy = "AdminPolicy")] // 
        public async Task<IActionResult> GetAllPaymentInfo([FromBody] string department, [FromHeader] string Authorization)
        {
            if (department.ToLower() != "tsedey bank") return NoContent();

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
        public async Task<IActionResult> GetAllPaymentChannel([FromHeader] string Authorization)
        {
            var PymentInfo = await this._payment.Set<PaymentChannel>().ToArrayAsync();

            if (PymentInfo.Length <= 0)
                return NoContent();

            return Ok(new JsonResult(PymentInfo).Value);
        }
        [HttpGet("payment-type")]
        public async Task<IActionResult> GetAllPaymentType([FromHeader] string Authorization)
        {
            
            var PymentInfo = await this._payment.Set<PaymentType>().ToArrayAsync();

            if (PymentInfo.Length <= 0)
                return NoContent();

            return Ok(new JsonResult(PymentInfo).Value);
        }

        [HttpGet("payment-purpose")]
        public async Task<IActionResult> GetAllPaymentPurpose([FromHeader] string Authorization)
        {
            var PymentInfo = await this._payment.Set<PaymentPurpose>().ToArrayAsync();

            if (PymentInfo.Length <= 0)
                return NoContent();
            var user = this._tokenvalidate.setToken(Authorization.Split(" ")[1]).getUserName();

            return Ok(new JsonResult(PymentInfo).Value);
        }

        [HttpPost("payment-type")]
        // admin / supervisors
        public async Task<IActionResult> SetPaymentType([FromBody] PaymentTypeReg paymentType, [FromHeader] string Authorization)
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
                CreatedOn = DateTime.Now,
                UpdatedOn = null,
                UpdatedBy = "",
            };

            await this._payment.AddAsync<PaymentType>(type);
            await this._payment.SaveChangesAsync();

            return Created("/", new JsonResult(type).Value);
        }

        [HttpPost("payment-channel")]
        // admin / supervisors
        public async Task<IActionResult> SetPaymentChannels([FromBody] PaymentChannelReg paymentChannel, [FromHeader] string Authorization)
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
                CreatedOn = DateTime.Now,
                UpdatedOn = null,
                UpdatedBy = "",
            };

            await this._payment.AddAsync<PaymentChannel>(Channel);
            await this._payment.SaveChangesAsync();

            return Created("/", new JsonResult(Channel).Value);
        }

        [HttpPost("payment-purpose")]
        // admin / supervisors
        public async Task<IActionResult> SetPaymentPurpose([FromBody] PaymentPurposeReg paymentPurpose,[FromHeader] string Authorization)
        {
            try
            {
                var username = await this._userManager.FindByNameAsync(paymentPurpose.CreatedBy); // Check if the user exists
                if (username == null)
                    throw new Exception("USER NOT FOUND.");
                if (paymentPurpose.Purpose.Count()<=0)
                    throw new Exception("EMPTY FEILD.");

                for (var i = 0; i < paymentPurpose.Purpose.Count(); i++)
                {
                    var PymentInfo = await this._payment.PaymentPurposes.Where((e) => e.Purpose == paymentPurpose.Purpose[i]).ToArrayAsync();

                    PaymentPurpose purpose = new PaymentPurpose
                    {
                        Purpose = paymentPurpose.Purpose[i],
                        Amount = paymentPurpose.Amount[i],
                        CreatedBy = paymentPurpose.CreatedBy,
                        CreatedOn = DateTime.Now,
                        UpdatedOn = null,
                        UpdatedBy = null,
                    };

                    if (PymentInfo.Length <= 0)
                        await this._payment.AddAsync(purpose);

                    await this._payment.SaveChangesAsync();
                }

                return Created("/", new JsonResult(paymentPurpose).Value);

            }catch(Exception ex)
            {
                return BadRequest(new { msg=$"PAYMENT PURPOSE UPLOAD FAILED! :: {ex.Message}"});
            }
        }
        //Update

        [HttpPut("payment-type")]
        // admin / supervisors
        public async Task<IActionResult> updatePaymentType([FromBody] PaymentTypeUpdate paymentType, [FromHeader] string Authorization)
        {
            var username = await this._userManager.FindByNameAsync(paymentType.UpdatedBy); // Check if the user exists
            if (username == null)
                return NotFound("User not found");

            var PymentInfo = await this._payment.Set<PaymentType>()
                                  .Where<PaymentType>((type) => type.Id == paymentType.id)
                                  .ExecuteUpdateAsync(e => e.SetProperty(e => e.type, paymentType.type));

            await this._payment.SaveChangesAsync();
            return Ok($"Updated - payment channel to {paymentType.type}");
        }

        [HttpPut("payment-channel")]
        // admin / supervisors
        public async Task<IActionResult> updatePaymentChannels([FromBody] PaymentChannelUpdate paymentChannel, [FromHeader] string Authorization)
        {
            var username = await this._userManager.FindByNameAsync(paymentChannel.UpdatedBy); // Check if the user exists
            if (username == null)
                return NotFound("User not found");

            var PymentInfo = await this._payment.Set<PaymentChannel>()
                .Where<PaymentChannel>((type) => type.Id == paymentChannel.id)
                .ExecuteUpdateAsync(e => e.SetProperty(e => e.Channel, paymentChannel.Channel));

            await this._payment.SaveChangesAsync();

            return Ok($"Updated - payment channel to {paymentChannel.Channel}");
        }

        [HttpPut("payment-purpose")]
        // admin / supervisors
        public async Task<IActionResult> updatePaymentPurpose([FromBody] PaymentPurposeUpdate paymentPurpose, [FromHeader] string Authorization)
        {
            var username = await this._userManager.FindByNameAsync(paymentPurpose.UpdatedBy); // Check if the user exists
            if (username == null)
                return NotFound("User not found");

            var PymentInfo = await this._payment.Set<PaymentPurpose>()
                .Where<PaymentPurpose>((e) => e.Id == paymentPurpose.id)
                .ExecuteUpdateAsync(e => e.SetProperty(e => e.Purpose, paymentPurpose.Purpose));

            await this._payment.SaveChangesAsync();

            return Ok($"Updated - payment purpose to {paymentPurpose.Purpose}");
        }

        // delete

        [HttpDelete("payment-type")]
        // admin / supervisors
        public async Task<IActionResult> deletePaymentType([FromBody] PaymentTypeDelete paymentType, [FromHeader] string Authorization)
        {
            var username = await this._userManager.FindByNameAsync(paymentType.deletedBy); // Check if the user exists
            if (username == null)
                return NotFound("User not found");

            var PymentInfo = await this._payment.Set<PaymentType>()
                                  .Where<PaymentType>((type) => type.Id == paymentType.id)
                                  .ExecuteDeleteAsync();

            await this._payment.SaveChangesAsync();
            return Ok($"Deleted - payment type");
        }

        [HttpDelete("payment-channel")]
        // admin / supervisors
        public async Task<IActionResult> deletePaymentChannels([FromBody] PaymentChannelDelete paymentChannel, [FromHeader] string Authorization)
        {
            var username = await this._userManager.FindByNameAsync(paymentChannel.deletedBy); // Check if the user exists
            if (username == null)
                return NotFound("User not found");

            var PymentInfo = await this._payment.Set<PaymentChannel>()
                .Where<PaymentChannel>((type) => type.Id == paymentChannel.id)
                .ExecuteDeleteAsync();

            await this._payment.SaveChangesAsync();

            return Ok($"Deleted - payment Channel");
        }

        [HttpDelete("payment-purpose")]
        // admin / supervisors
        public async Task<IActionResult> deletePaymentPurpose([FromBody] PaymentPurposeDelete paymentPurpose, [FromHeader] string Authorization)
        {
            var username = await this._userManager.FindByNameAsync(paymentPurpose.deletedBy); // Check if the user exists
            if (username == null)
                return NotFound("User not found");

            var PymentInfo = await this._payment.Set<PaymentPurpose>()
                .Where<PaymentPurpose>((e) => e.Id == paymentPurpose.id)
                .ExecuteDeleteAsync();

            await this._payment.SaveChangesAsync();

            return Ok($"Deleted - payment Purpose");
        }
        [HttpGet("redirecttoboa")]
        public IActionResult RedirectToSlip(string transactionId)
        {
            var slipUrl = $"https://cs.bankofabyssinia.com/slip/?trx={WebUtility.UrlEncode(transactionId)}";
            return Redirect(slipUrl);
        }

        [HttpGet("slip")]
        public async Task<IActionResult> GetSlip([FromQuery] string trx)
        {
            try
            {
                // Create the HttpClient
                //  var client = _httpClientFactory.CreateClient("BankOfAbyssinia");
                HttpClient client = new HttpClient();

                // Encode the `trx` parameter to ensure URL safety
                string encodedTrx = WebUtility.UrlEncode(trx);
                string requestUrl = $"https://cs.bankofabyssinia.com/slip/?trx={encodedTrx}";

                // Send the GET request
                var response = await client.GetAsync(requestUrl);

                // Handle the response
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Ok(content); // Return the external service's response
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to fetch data from external service.");
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception (e.g., using ILogger)
                return StatusCode(500, "Error connecting to the external service.");
            }
        }



        // register hospitals
        [HttpGet("hospitals")]
        public async Task<IActionResult> GetHospitals([FromHeader] string Authorization)
        {
            var PymentInfo = await this._payment.Set<Hospital>().ToArrayAsync();
            if (PymentInfo.Length <= 0)
            {
                return NoContent();
            }
            return Ok(new JsonResult(PymentInfo).Value);
        }
        [HttpPost("hospital")]
        public async Task<IActionResult> InsertHospitals([FromBody] HospitalReg hospital, [FromHeader] string Authorization)
        {
            var username = await this._userManager.FindByNameAsync(hospital.RegisteredBy);
            if (username == null)
                return NotFound("User not found");

            try
            {
                Hospital hospital_reg = new Hospital
                {
                    HospitalName = hospital.HospitalName,
                    HospitalManager = hospital.HospitalManager,
                    Email = hospital.Email,
                    Phone = hospital.Phone,
                    Location = hospital.Location,
                    ContactMethod = hospital.ContactMethod,
                    RegisteredOn = DateTime.UtcNow,
                    RegisteredBy = hospital.RegisteredBy
                };
                await this._payment.AddAsync(hospital_reg);
                await this._payment.SaveChangesAsync();
                return Created($"/{hospital.HospitalName}", new JsonResult(hospital_reg).Value);
            }catch(Exception ex)
            {
                return BadRequest($"Registration Failed! : {ex.Message}");
            }
        }
        [HttpDelete("hospital")]
        public async Task<IActionResult> DeleteHospitals([FromBody] HospitalDelete hospitalDelete, [FromHeader] string Authorization)
        {
            var username = await this._userManager.FindByNameAsync(hospitalDelete.user);
            if (username == null)
                return NotFound("User not found");
            var delete = await this._payment.Set<Hospital>().Where(e=> e.Id==hospitalDelete.id).ExecuteDeleteAsync();
            return Ok("Hospital information deleted Deleted!");
        }

        [HttpPut("hospital")]
        public async Task<IActionResult> UpdateHospitals([FromBody] HospitalUpdate hospitalUpdate, [FromHeader] string Authorization)
        {
            var username = await this._userManager.FindByNameAsync(hospitalUpdate.user);
            if (username == null)
                return NotFound("User not found");
            var hospital = await this._payment.Set<Hospital>()
                .Where((e) => e.Id == hospitalUpdate.id)
                .ExecuteUpdateAsync(e => 
                        e.SetProperty(e => e.HospitalManager, hospitalUpdate.HospitalManager)
                        .SetProperty(e => e.ContactMethod, hospitalUpdate.ContactMethod)
                        .SetProperty(e => e.Email, hospitalUpdate.Email)
                        .SetProperty(e => e.Phone, hospitalUpdate.Phone)
                        .SetProperty(e => e.Location, hospitalUpdate.Location)
                );

            await this._payment.SaveChangesAsync();
            return Ok($"Updated - Hospital information");
        }
        private class BankLinkList
        {

            public string? Institution { get; set; }
            public string? QRLink { get; set; }
        }

    }
}
