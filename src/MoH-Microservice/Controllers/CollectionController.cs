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
                                   e.CreatedOn <= collectionReg.ToDate.Date)
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
            var collectionList = await this._collection.Set<PCollections>().Where(col => col.Casher == username).ToArrayAsync();
            
            if (collectionList.Count() <=0) return NoContent();   
            
            return Ok(collectionList);
        }

    }
}
