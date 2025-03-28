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

                return Created("/", new { msg="Cash is collected!",data=Collection});
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

    }
}
