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
    public class CollectionController : ControllerBase
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly AppDbContext _collection;


        public CollectionController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext collection)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._collection = collection;
        }

        [HttpPost("collection")]
        public async Task<IActionResult> UpdatePaymentCollectionStatus(CollectionReg collectionReg)
        {
            var user = await this._userManager.FindByNameAsync(collectionReg.Casher);
            if (user == null)
                return NotFound("User not found");

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
                var result = await this._collection.Set<PaymentCollectors>().Where(e => e.EmployeeID == collectionReg.CollecterID && e.EmployeeName == collectionReg.CollectedBy && e.AssignedLocation == user.Hospital).ToArrayAsync();
                if (result.Length <= 0)
                {
                    return NotFound("Collector not found!");
                }

                var Collection = new PCollections
                {
                    FromDate = collectionReg.FromDate.Date,
                    ToDate = collectionReg.ToDate.Date,
                    Casher = collectionReg.Casher,
                    CollectedBy = collectionReg.CollectedBy,
                    CollecterID = collectionReg.CollecterID,
                    CollectedOn = collectionReg.CollectedOn,
                    CollectedAmount = collectionReg.CollectedAmount
                };

                await this._collection.AddAsync(Collection);
                await this._collection.SaveChangesAsync();

                /**
                 * Confirm For :- 
                  1. Collector [Banker] (optional)
                  2. Employee [Cashier] (optional)
                  3. District [Bankers Supervisor] (Mandatory)
                  4. Hospiatal Manager [Cashiers supervisor] (Mandatory)
                 */

                bool confim = await Confirmation(user.Hospital,collectionReg);

                return Created("/", new { msg="Cash is collected!",data=Collection,confimationSent = confim });
            }  
        }

        [HttpPost("register_collector")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task <IActionResult> register_collector(PaymentCollectorsReg collector)
        {
            var user = await this._userManager.FindByNameAsync(collector.User);
            if (user == null)
                return NotFound("User not found");
            try
            {   // before the 
                PaymentCollectors banker = new PaymentCollectors
                {
                    EmployeeID = collector.EmployeeID,
                    EmployeeName = collector.EmployeeName,
                    EmployeePhone = collector.EmployeePhone,
                    EmployeeEmail = collector.EmployeeEmail,
                    AssignedLocation = collector.AssignedLocation,
                    AssignedAs = collector.AssignedAs,
                    AssignedOn = DateTime.Now,
                    AssignedBy = collector.AssignedBy
                };

                this._collection.AddAsync<PaymentCollectors>(banker);
                this._collection.SaveChangesAsync();

                // send email
                
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
                var result = await this._collection.Set<PaymentCollectors>().Where(e => e.EmployeeID == collector.EmployeeID && e.EmployeeName == e.EmployeeName && e.AssignedLocation == user.Hospital).ToArrayAsync();
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

        [HttpGet("collector/{username}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetListOfCollector([FromRoute] string username)
        {
            var user = await this._userManager.FindByNameAsync(username);
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


        [HttpGet("collection/{username}")]

        public async Task<IActionResult> ListOfCollections([FromRoute] string username)
        {
            var user = await this._userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound("User not found");
            var collectionList = await this._collection.Set<PCollections>().Where(col => col.Casher == username).Take(100).ToArrayAsync();

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
            Console.WriteLine($"Email been sent to {email} {content}");

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

            foreach( var item in result)
            {
                var text = $"Dear {item.EmployeeName}\r\n" +
                    $"{collection.CollectedAmount} Birr has been " +
                    $"Collected by [ {collection.CollectedBy} ]" +
                    $"On [ {collection.CollectedOn} ]" +
                    $"from [ {collection.Casher} ]\r\n" +
                    $"Tsedey Bank OTC-MOHSystem [{DateTime.Now}] ";
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
