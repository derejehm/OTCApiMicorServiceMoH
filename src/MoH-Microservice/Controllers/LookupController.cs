
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MoH_Microservice.Data;
using MoH_Microservice.Misc;
using MoH_Microservice.Models.Database;
using MoH_Microservice.Models.Form;
using MoH_Microservice.Query;
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
        private TokenValidate _tokenValidate;
        private AppQuery _appQuery;
        

        public LookupController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext payment
          
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._payment = payment;
            this._tokenValidate = new TokenValidate(userManager);
            this._appQuery = new AppQuery(this._payment);


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
        public async Task<IActionResult> GetAllPaymentInfo([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                if (user.Departement.ToLower() != "tsedey bank") return NoContent();

                var PymentInfo = await this._payment.Set<Payment>()
                                .GroupBy((col) => new { Hospital = col.HospitalName, Casher = col.CreatedBy, Type = col.Type, Purpose = col.Purpose })
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
            }catch(Exception ex)
            {
                return BadRequest(new { msg = $"Error: fetch payment failed! Reason: {ex.Message}" });
            }

        }

        [HttpGet("payment-channel")]
        public async Task<IActionResult> GetAllPaymentChannel([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._payment.Set<PaymentChannel>().ToArrayAsync();

                if (PymentInfo.Length <= 0)
                    return NoContent();
                return Ok(new JsonResult(PymentInfo).Value);

            }catch(Exception ex)
            {
                return BadRequest(new { msg = $"Error: Insert Providers failed! Reason: {ex.Message}" });
            }
        }
        [HttpGet("payment-type")]
        public async Task<IActionResult> GetAllPaymentType([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._payment.Set<PaymentType>().ToArrayAsync();

                if (PymentInfo.Length <= 0)
                    return NoContent();

                return Ok(new JsonResult(PymentInfo).Value);

            }catch(Exception ex)
            {
                return BadRequest(new { msg = $"Error: fetch payment-type failed! Reason: {ex.Message}" });
            }

        }

        [HttpGet("payment-purpose")]
        public async Task<IActionResult> GetAllPaymentPurpose([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._payment.Set<PaymentPurpose>().ToArrayAsync();

                if (PymentInfo.Length <= 0)
                    return NoContent();

                return Ok(new JsonResult(PymentInfo).Value);
            }catch(Exception ex)
            {
                return BadRequest(new { msg = $"Error: fetch payment-purpose failed! Reason: {ex.Message}" });
            }

        }

        [HttpPost("payment-type")]
        // admin / supervisors
        public async Task<IActionResult> SetPaymentType([FromBody] PaymentTypeReg paymentType, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._payment.Set<PaymentType>().Where<PaymentType>((type) => type.type == paymentType.type).ToArrayAsync();

                if (PymentInfo.Length > 0)
                    return BadRequest("Payment type aleady exist");

                PaymentType type = new PaymentType
                {
                    type = paymentType.type,
                    CreatedBy = user.UserName,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = null,
                    UpdatedBy = "",
                };

                await this._payment.AddAsync<PaymentType>(type);
                await this._payment.SaveChangesAsync();

                return Created("/", new JsonResult(type).Value);
            }catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: Insert payment-type failed! Reason: {ex.Message}" });
            }


        }

        [HttpPost("payment-channel")]
        // admin / supervisors
        public async Task<IActionResult> SetPaymentChannels([FromBody] PaymentChannelReg paymentChannel, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._payment.PaymentChannels.Where((type) => type.Channel == paymentChannel.Channel).ToArrayAsync();

                if (PymentInfo.Length > 0)
                    throw new Exception("Payment channel aleady exist");

                PaymentChannel Channel = new PaymentChannel
                {
                    Channel = paymentChannel.Channel,
                    CreatedBy = user.UserName,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = null,
                    UpdatedBy = "",
                };

                await this._payment.AddAsync<PaymentChannel>(Channel);
                await this._payment.SaveChangesAsync();

                return Created("/", new JsonResult(Channel).Value);
            }catch(Exception ex)
            {
                return BadRequest(new { msg = $"Error: Insert payment-channel failed! Reason: {ex.Message}" });
            }

        }

        [HttpPost("payment-purpose")]
        // admin / supervisors
        public async Task<IActionResult> SetPaymentPurpose([FromBody] PaymentPurposeReg paymentPurpose,[FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                
                if (paymentPurpose.Purpose.Count()<=0)
                    throw new Exception("EMPTY FEILD.");

                for (var i = 0; i < paymentPurpose.Purpose.Count(); i++)
                {
                    var PymentInfo = await this._payment.PaymentPurposes
                                    .Where((e) => e.Purpose == paymentPurpose.Purpose[i])
                                    .ToArrayAsync();

                    if (PymentInfo.Count() > 0)
                    {
                        
                        await this._payment
                            .PaymentPurposes
                            .Where((e) => e.Purpose == paymentPurpose.Purpose[i])
                            .ExecuteUpdateAsync(u => u
                            .SetProperty(p => p.Amount, paymentPurpose.Amount[i])
                            .SetProperty(p => p.shortCodes, paymentPurpose.shortCode[i])
                            .SetProperty(p => p.group, paymentPurpose.group[i])
                            .SetProperty(p => p.subgroup, paymentPurpose.subgroup[i])
                            .SetProperty(p=>p.UpdatedBy,user.UserName)
                            .SetProperty(p=>p.UpdatedOn,DateTime.Now)
                            );
                            
                        continue;
                    }
                        
                    PaymentPurpose purpose = new PaymentPurpose
                    {
                        Purpose = paymentPurpose.Purpose[i],
                        shortCodes = paymentPurpose.shortCode[i],
                        group = paymentPurpose.group[i],
                        subgroup = paymentPurpose.subgroup[i],
                        Amount =  paymentPurpose.Amount[i],
                        CreatedBy = user.UserName,
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
                return BadRequest(new AppError { msg=$"PAYMENT PURPOSE UPLOAD FAILED! :: {ex.Message}", ErrorDescription=ex.Message});
            }
        }
        //Update

        [HttpPut("payment-type")]
        // admin / supervisors
        public async Task<IActionResult> updatePaymentType([FromBody] PaymentTypeUpdate paymentType, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._payment.Set<PaymentType>()
                                      .Where<PaymentType>((type) => type.Id == paymentType.id)
                                      .ExecuteUpdateAsync(e => e.SetProperty(e => e.type, paymentType.type));

                await this._payment.SaveChangesAsync();
                return Ok($"Updated - payment channel to {paymentType.type}");

            }catch(Exception ex)
            {
                return BadRequest(new { msg = $"Error: update payment-type failed! Reason: {ex.Message}" });
            }


        }

        [HttpPut("payment-channel")]
        // admin / supervisors
        public async Task<IActionResult> updatePaymentChannels([FromBody] PaymentChannelUpdate paymentChannel, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var PymentInfo = await this._payment.Set<PaymentChannel>()
                    .Where<PaymentChannel>((type) => type.Id == paymentChannel.id)
                    .ExecuteUpdateAsync(e => e.SetProperty(e => e.Channel, paymentChannel.Channel));

                await this._payment.SaveChangesAsync();

                return Ok($"Updated - payment channel to {paymentChannel.Channel}");
            }catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: update payment-channel failed! Reason: {ex.Message}" });
            }

        }

        [HttpPut("payment-purpose")]
        // admin / supervisors
        public async Task<IActionResult> updatePaymentPurpose([FromBody] PaymentPurposeUpdate paymentPurpose, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._payment.PaymentPurposes
                    .Where((e) => e.Id == paymentPurpose.id)
                    .ExecuteUpdateAsync(e => e.SetProperty(e => e.Purpose, paymentPurpose.Purpose));

                await this._payment.SaveChangesAsync();

                return Ok($"Updated - payment purpose to {paymentPurpose.Purpose}");
            }catch(Exception ex)
            {
                return BadRequest(new { msg = $"Error: update payment-purpose failed! Reason: {ex.Message}" });
            }


        }
        // delete

        [HttpDelete("payment-type")]
        // admin / supervisors
        public async Task<IActionResult> deletePaymentType([FromBody] PaymentTypeDelete paymentType, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var PymentInfo = await this._payment.Set<PaymentType>()
                                      .Where<PaymentType>((type) => type.Id == paymentType.id)
                                      .ExecuteDeleteAsync();

                await this._payment.SaveChangesAsync();
                return Ok($"Deleted - payment type");

            }catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: Delete payment-type failed! Reason: {ex.Message}" });
            }

        }

        [HttpDelete("payment-channel")]
        // admin / supervisors
        public async Task<IActionResult> deletePaymentChannels([FromBody] PaymentChannelDelete paymentChannel, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._payment.Set<PaymentChannel>()
                    .Where<PaymentChannel>((type) => type.Id == paymentChannel.id)
                    .ExecuteDeleteAsync();

                await this._payment.SaveChangesAsync();

                return Ok($"Deleted - payment Channel");

            }catch (Exception ex)
            {
                return BadRequest(new { msg = $"payment-channel Providers failed! Reason: {ex.Message}" });
            }

        }

        [HttpDelete("payment-purpose")]
        // admin / supervisors
        public async Task<IActionResult> deletePaymentPurpose([FromBody] PaymentPurposeDelete paymentPurpose, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._payment.Set<PaymentPurpose>()
                    .Where<PaymentPurpose>((e) => e.Id == paymentPurpose.id)
                    .ExecuteDeleteAsync();

                await this._payment.SaveChangesAsync();

                return Ok($"Deleted - payment Purpose");
            }catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: Delete payment-purpose failed! Reason: {ex.Message}" });
            }
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
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._payment.Set<Hospital>().ToArrayAsync();
                if (PymentInfo.Length <= 0)
                {
                    return NoContent();
                }
                return Ok(new JsonResult(PymentInfo).Value);
            }catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: fetch hospitals failed! Reason: {ex.Message}" });
            }
        }

        [HttpPost("hospital")]
        public async Task<IActionResult> InsertHospitals([FromBody] HospitalReg hospital, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                Hospital hospital_reg = new Hospital
                {
                    HospitalName = hospital.HospitalName,
                    HospitalManager = hospital.HospitalManager,
                    Email = hospital.Email,
                    Phone = hospital.Phone,
                    Location = hospital.Location,
                    ContactMethod = hospital.ContactMethod,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = user.UserName
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
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var delete = await this._payment.Set<Hospital>().Where(e=> e.Id==hospitalDelete.id).ExecuteDeleteAsync();
                return Ok(new { msg = "Hospital information deleted Deleted!" });
            }catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: Delete hospital failed! Reason: {ex.Message}" });
            }
        }

        [HttpPut("hospital")]
        public async Task<IActionResult> UpdateHospitals([FromBody] HospitalUpdate hospitalUpdate, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
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
            }catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: update hospital failed! Reason: {ex.Message}" });
            }
        }
        //----
        [HttpGet("payment-type-description")]
        public async Task<IActionResult> GetAllPaymentPurposeDiscription([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._appQuery.PaymentTypeQuery()
                    .GroupBy(g=> new { g.Type, g.TypeId })
                    .Select(s=> new PaymentTypeDTO2
                    {
                        Id=s.FirstOrDefault().TypeId,
                        Type=s.FirstOrDefault().Type,
                        Description=s.Select(s=> new PaymentTypeDiscriptionDTO { Id=s.DescriptionId, Description=s.Description}).ToArray()
                    })
                    .ToArrayAsync();

                if (PymentInfo.Length <= 0)
                    return NoContent();

                return Ok(new { data=new JsonResult(PymentInfo).Value });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: fetch payment-purpose-discription failed! Reason: {ex.Message}" });
            }

        }
        [HttpDelete("payment-type-description")]
        // admin / supervisors
        public async Task<IActionResult> deletePaymentPurposeDiscription([FromBody] PaymentPurposeDelete paymentPurposedis, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._payment.PaymentTypeDiscriptions
                    .Where((e) => e.Id == paymentPurposedis.id)
                    .ExecuteDeleteAsync();

                await this._payment.SaveChangesAsync();

                return Ok(new { msg = $"Deleted - payment Purpose" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: Delete payment-purpose-discription failed! Reason: {ex.Message}" });
            }
        }
        [HttpPut("payment-type-description")]
        // admin / supervisors
        public async Task<IActionResult> updatePaymentPurposeDiscription([FromBody] PaymentPurposeUpdateDiscription paymentPurpose, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this._payment.PaymentTypeDiscriptions
                    .Where((e) => e.Id == paymentPurpose.id)
                    .ExecuteUpdateAsync(e => e
                    .SetProperty(e => e.Discription, paymentPurpose.Discription)
                    .SetProperty(e=>e.UpdatedOn,DateTime.Now)
                    .SetProperty(e=>e.UpdatedBy,user.UserName)
                    );

                await this._payment.SaveChangesAsync();

                return Ok(new { msg=$"Updated - payment purpose to {paymentPurpose.Discription}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"Error: update payment-purpose--discription failed! Reason: {ex.Message}" });
            }
        }
        [HttpPost("payment-type-description")]
        // admin / supervisors
        public async Task<IActionResult> SetPaymentPurposeDiscription([FromBody] PaymentPurposDiscription paymentType, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                if (paymentType?.Discription?.Count() <= 0)
                    throw new Exception("EMPTY FEILD.");
                var PymentInfo = await this._payment.PaymentTypes.Where((e) => e.type == paymentType.Type).ToArrayAsync();
                if (PymentInfo.Length <= 0)
                {
                    throw new Exception("Payment Purpose Does not exist");
                }
                for (var i = 0; i < paymentType.Discription.Count(); i++)
                {
                    

                    PaymentTypeDiscription purpose = new PaymentTypeDiscription
                    {
                        PaymentTypeID = PymentInfo?.FirstOrDefault()?.Id,
                        Discription = paymentType.Discription[i],
                        CreatedBy = user.UserName,
                        CreatedOn = DateTime.Now,
                        UpdatedOn = null,
                        UpdatedBy = null,
                    };

                    if (PymentInfo?.Length > 0)
                        await this._payment.AddAsync(purpose);

                    await this._payment.SaveChangesAsync();
                }
                var result =  await this._appQuery.PaymentTypeQuery()
                    .Where(w=>w.TypeId == PymentInfo.FirstOrDefault().Id)
                    .GroupBy(g => new { g.Type, g.TypeId })
                    .Select(s => new PaymentTypeDTO2
                    {
                        Id = s.FirstOrDefault().TypeId,
                        Type = s.FirstOrDefault().Type,
                        Description = s.Select(s => new PaymentTypeDiscriptionDTO { Id = s.DescriptionId, Description = s.Description }).ToArray()
                    })
                    .ToArrayAsync();
                return Created("/", new AppSuccess { data = new JsonResult(result) });

            }
            catch (Exception ex)
            {
                return BadRequest(new AppError{ ErrorDescription = ex.Message});
            }
        }
        private class BankLinkList
        {

            public string? Institution { get; set; }
            public string? QRLink { get; set; }
        }

    }
}
