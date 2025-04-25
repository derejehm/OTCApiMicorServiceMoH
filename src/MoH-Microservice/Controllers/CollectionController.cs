using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoH_Microservice.Data;
using MoH_Microservice.Models;
using System.Globalization;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollectionController : ControllerBase
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly AppDbContext _collection;
        public readonly IEmailSender _emailSender;

        public CollectionController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext collection,IEmailSender emailSender)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._collection  = collection;
            this._emailSender = emailSender;
        }

        [HttpPost("collection")]
        public async Task<IActionResult> UpdatePaymentCollectionStatus(CollectionReg collectionReg)
        {
            var user = await this._userManager.FindByNameAsync(collectionReg.Casher);
            if (user == null)
                return NotFound("User not found");

            var result = await this._collection.Set<PaymentCollectors>()
                .Where(e => e.EmployeeID.ToLower() == collectionReg.CollecterID.ToLower() && e.EmployeeName.ToLower() == collectionReg.CollectedBy.ToLower() && e.AssignedLocation.ToLower() == user.Hospital.ToLower()).ToArrayAsync();
            if (result.Length <= 0)
            {
                return NotFound("Collector not found!");
            }

            var Query = this._collection.Set<Payment>()
                            .Where(e =>
                                   e.Createdby == collectionReg.Casher &&
                                   e.IsCollected != 1 &&
                                   e.Type.ToLower() == "cash" &&
                                   e.CreatedOn >= collectionReg.FromDate.Date &&
                                   e.CreatedOn <= collectionReg.ToDate.Date.AddDays(1))
                            .ExecuteUpdateAsync(update => update.SetProperty(item => item.IsCollected, 1)).Result;

            if (Query <= 0)
            {
                return NotFound("Could't find any uncollected cash!");
            }
            else {
                // check if the collector is assigned to collect from the hospital
                

                var Collection = new PCollections
                {
                    FromDate = collectionReg.FromDate.Date,
                    ToDate = collectionReg.ToDate.Date,
                    Casher = collectionReg.Casher,
                    CollectedBy = collectionReg.CollectedBy,
                    CollecterID = collectionReg.CollecterID,
                    CollectedOn = collectionReg.CollectedOn,
                    CollectedAmount = collectionReg.CollectedAmount,
                    CreatedBy=user.UserName,
                    CreatedOn=DateTime.Now,
                };

                await this._collection.AddAsync(Collection);
                await this._collection.SaveChangesAsync();

                /**
                 * Confirm For :- 
                  1. Collector [Banker] (optional)
                  2. Employee  [Cashier] (optional)
                  3. District  [Bankers Supervisor] (Mandatory)
                  4. Hospiatal Manager [Cashiers supervisor] (Mandatory)
                 */

                bool confim = await Confirmation(user.Hospital,collectionReg);

                return Created("/", new { msg="Cash is collected!",data=Collection,confimationSent = confim });
            }  
        }

        [HttpPost("register_collector")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task <IActionResult> register_collector([FromBody] PaymentCollectorsRegArray collector)
        {
            var user = await this._userManager.FindByNameAsync(collector.User);
            if (user == null)
                return NotFound("User not found");
            try
            {   // before the
                var reuslt  = await this._collection.Set< PaymentCollectors >().ExecuteDeleteAsync();
                // delete 
                for (var i=0;i<collector.EmployeeID.Count();i++){

                    PaymentCollectors banker = new PaymentCollectors
                    {
                        EmployeeID = collector.EmployeeID[i],
                        EmployeeName = collector.EmployeeName[i],
                        EmployeePhone = collector.EmployeePhone[i],
                        EmployeeEmail = collector.EmployeeEmail[i],
                        AssignedLocation = user.Hospital,
                        AssignedAs = collector.AssignedAs[i],
                        ContactMethod = collector.ContactMethod[i],
                        AssignedOn = DateTime.Now,
                        AssignedBy = collector.AssignedBy[i]
                    };
                    var text = $"\r\nDear {collector.EmployeeName[i]} " +
                        $"\nYou have been assigned to collect cash from {user.Hospital} Hospital " +
                        $"by {collector.AssignedBy[i]}";
                    if (collector.ContactMethod[i].ToLower() == "email")
                    {
                        SendConfirmationEmail(collector.EmployeeEmail[i], text);
                    }
                    if (collector.ContactMethod[i].ToLower() == "sms")
                    {
                        SendConfirmationSMS(collector.EmployeePhone[i], text);
                    }
                    
                    await this._collection.AddAsync<PaymentCollectors>(banker);
                    await this._collection.SaveChangesAsync();
                }
                
                return Created("/",collector);
            }catch(Exception ex)
            {
                return BadRequest($"Registration failed ! message/ reason : {ex}");
            }
        }

        [HttpPut("collector-check")]
        public async Task<IActionResult> GetCollector(PaymentCollectorsGetReq collector)
        {
            var user = await this._userManager.FindByNameAsync(collector.User);
            if (user == null)
                return NotFound("User not found");
            try
            {
                var result = await this._collection.PaymentCollectors
                            .Where(e => e.EmployeeID.ToLower() == collector.EmployeeID.ToLower() 
                                && e.EmployeeName.ToLower() == collector.EmployeeName.ToLower() 
                                && e.AssignedLocation.ToLower() == user.Hospital.ToLower())
                    .ToArrayAsync();
                if (result.Length <= 0)
                {
                    return NotFound("Employee Not Found!");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Feacting failed ! message/ reason : {ex}");
            }
        }

        [HttpGet("collector/{loggedUser}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetListOfCollector([FromRoute] string loggedUser)
        {
            var user = await this._userManager.FindByNameAsync(loggedUser);
            if (user == null)
                return NotFound("User not found");
            try
            {
                var result = await this._collection.Set<PaymentCollectors>().ToArrayAsync();
                if (result.Length <= 0)
                {
                    return NoContent();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Feacting data failed! message/ reason : {ex}");
            }
        }


        [HttpGet("collection/{loggedUser}")]

        public async Task<IActionResult> ListOfCollections([FromRoute] string loggedUser)
        {
            var user = await this._userManager.FindByNameAsync(loggedUser);
            if (user == null)
                return NotFound("User not found");
            var collectionList = await this._collection.Set<PCollections>().Where(col => col.Casher == user.UserName).Take(100).ToArrayAsync();

            if (collectionList.Count() <= 0) return NoContent(); // there is no collected cash

            return Ok(collectionList);
        }

        [HttpGet("uncollected/{username}")]

        public async Task<IActionResult> ListOfUncollected([FromRoute] string username)
        {
            var user = await this._userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound("User not found");
            var collectionList = await this._collection.Set<Payment>()
                                 .Where(col => col.Createdby == username && 
                                               col.IsCollected!=1 && 
                                               col.Type.ToLower()=="cash")
                                 .GroupBy(e=>new { e.Createdby, e.IsCollected})
                                 .Select(e => new {
                                     Cashier=e.Key.Createdby,
                                     IsCollected=e.Key.IsCollected,
                                     UncollectedCashAmount =e.Sum(e=>e.Amount),
                                     FromDate= e.Min(e=>e.CreatedOn),
                                     ToDate= e.Max(e=>e.CreatedOn)
                                 })
                                 .ToArrayAsync();

            if (collectionList.Count() <= 0) return NoContent();

            return Ok(collectionList);
        }

        [HttpGet("rpt-uncollected/{loggedinuser}")]

        public async Task<IActionResult> UncollectedByuser([FromRoute] string loggedinuser)
        {
            var user = await this._userManager.FindByNameAsync(loggedinuser);
            if (user == null)
                return NotFound("User not found");
            
            if (user.UserType.ToLower() == "cashier")
            {
                 var collectionList = await this._collection.Set<Payment>()
                                 .Where(col => col.Createdby == loggedinuser &&
                                               col.IsCollected != 1 &&
                                               col.Type.ToLower() == "cash")
                                 .GroupBy(e => new { e.Createdby, e.IsCollected })
                                 .Select(e => new {
                                     Cashier = e.Key.Createdby,
                                     CashAmount = e.Sum(e => e.Amount),
                                 })
                                 .ToArrayAsync();
                if (collectionList.Count() <= 0) return NoContent();

                return Ok(collectionList);
            }else if(user.UserType.ToLower() == "supervisor")
            {
                var collectionList = await this._collection.Set<Payment>()
                                 .Where(col => col.IsCollected != 1 && col.Type.ToLower() == "cash")
                                 .GroupBy(e => new { e.Createdby, e.IsCollected })
                                 .Select(e => new {Cashier = e.Key.Createdby,CashAmount = e.Sum(e => e.Amount)})
                                 .ToArrayAsync();

                if (collectionList.Count() <= 0) return NoContent();

                return Ok(collectionList);
            }
            else
            {
                return NoContent();
            } 
        }
        [HttpGet("get-last-collections/{loggedUser}")]
        public async Task<IActionResult> lastCollectedCash([FromRoute] string loggedUser)
        {
            var user = await this._userManager.FindByNameAsync(loggedUser); // Check if the user exists
            //var usersHttp = HttpContext.GetTokenAsync
            if (user == null)
                return NotFound("User not found");


            PCollections[] PymentInfo = [];
            if (user.UserType != "Supervisor")
            {
                PymentInfo = await this._collection.Set<PCollections>().Where(x=> x.Casher == user.UserName).OrderByDescending(e=>e.CollectedOn).Take(10).ToArrayAsync();

            }
            else
            {
                PymentInfo = await this._collection.Set<PCollections>().OrderByDescending(e => e.CollectedOn).Take(10).ToArrayAsync();

            }

            if (PymentInfo.Length <= 0)
                return NoContent();
            return Ok(new JsonResult(PymentInfo).Value);
        }

        [HttpPut("Get-all-Collection")]
        public async Task<IActionResult> GetPaymentByDate([FromBody] CollectionByDate collection)
        {
            var user = await this._userManager.FindByNameAsync(collection.user); // Check if the user exists
            //var usersHttp = HttpContext.GetTokenAsync
            if (user == null)
                return NotFound("User not found");


            PCollections[] PymentInfo = [];
            if (user.UserType != "Supervisor")
            {
                PymentInfo = await this._collection.Set<PCollections>().Where(x => x.CollectedOn.Date >= collection.startDate.Value.Date && x.CollectedOn.Date <= collection.endDate.Value.Date && x.Casher == user.UserName).ToArrayAsync();

            }
            else
            {
                PymentInfo = await this._collection.Set<PCollections>().Where(x => x.CollectedOn.Date >= collection.startDate.Value.Date && x.CollectedOn.Date <= collection.endDate.Value.Date).ToArrayAsync();

            }

            if (PymentInfo.Length <= 0)
                return NoContent();
            return Ok(new JsonResult(PymentInfo).Value);
        }

        private async void SendConfirmationEmail(string email, string content)
        {

            // email sending function
            try
            {
                await this._emailSender.SendEmailAsync(email, "Payment Collection", content);
            }catch (Exception ex)
            {
                Console.WriteLine($"Sending email to {email} failed! Reason : {ex.Message}"); 
            }
        }
        private async void SendConfirmationSMS(string phone, string content)
        {

            // email sending function
            Console.WriteLine($"SMS been sent to {phone} {content}");

        }

        private async Task<Boolean> Confirmation(string hospital, CollectionReg collection)
        {
            var result = await this._collection
                .Set<PaymentCollectors>()
                .Where(e =>e.AssignedAs.ToLower()=="supervisor" && e.AssignedLocation.ToLower()==hospital.ToLower())
                .Select(e=> new
                {
                    EmployeeEmail = e.EmployeeEmail
                   ,EmployeeName = e.EmployeeName
                   ,ContactMethod = e.ContactMethod
                })
                .ToArrayAsync();

            if (result == null || result.Length<=0)
            {
                return false;
            }
            var _hospital = await this._collection.Hospitals.Where(e => e.HospitalName.ToLower() == hospital.ToLower()).Select(e=> new
            {
                EmployeeEmail=e.Email,
                EmployeeName=e.HospitalManager,
                ContactMethod= e.ContactMethod
            }).ToArrayAsync();

            foreach (var item in result)
            {
                var text = $"\r\nDear {item.EmployeeName}\r\n" +
                    $"[ {collection.CollectedAmount} ] Birr has been " +
                    $"Collected by [ {collection.CollectedBy} ] " +
                    $"On [ {collection.CollectedOn} ] " +
                    $"from [ {collection.Casher} ]\r\n" +
                    $" - Tsedey Bank OTC-MOHSystem [{DateTime.Now}] ";
                if (item != null && item.ContactMethod.ToLower() == "email")
                {
                    text = $"<p>Dear <strong>{item.EmployeeName.ToUpper()}</strong>,</p>" +
                        $"<p><strong>{ collection.CollectedAmount.ToString("C",CultureInfo.InvariantCulture)} BIRR </strong> " +
                        $"has been Collected by <strong>{ collection.CollectedBy.ToUpper()}</strong> On <strong>{ collection.CollectedOn} </strong>  " +
                        $"from cashier <strong>{ collection.Casher.ToUpper()}</strong> at  <strong> {hospital.ToUpper()} </strong> hospital.</p>" +
                        $"<br/><p> -Tsedey Bank OTC-MOHSystem { DateTime.Now} </p> ";
                    SendConfirmationEmail(item.EmployeeEmail, text);
                }
                if (item != null && item.ContactMethod.ToLower() == "sms")
                {
                    SendConfirmationSMS(item.EmployeeEmail, text);
                }

            }

            foreach (var item in _hospital)
            {
                var text = $"\r\nDear {item.EmployeeName}\r\n" +
                    $"[ {collection.CollectedAmount} ] Birr has been " +
                    $"Collected by [ {collection.CollectedBy} ] " +
                    $"On [ {collection.CollectedOn} ] " +
                    $"from [ {collection.Casher} ]\r\n" +
                    $" - Tsedey Bank OTC-MOHSystem [{DateTime.Now}] ";
                if (item != null && item.ContactMethod.ToLower() == "email")
                {
                    SendConfirmationEmail(item.EmployeeEmail, text);
                }
                if (item != null && item.ContactMethod.ToLower() == "sms")
                {
                    SendConfirmationSMS(item.EmployeeEmail, text);
                }

            }



            return true;
        }
    }
}
