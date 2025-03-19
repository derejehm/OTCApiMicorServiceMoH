using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MoH_Microservice.Data;
using MoH_Microservice.Models;
using System.Linq;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class ReportController : ControllerBase
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly AppDbContext _report;
        public ReportController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext report
            ) {
        
                this._userManager = userManager;
                this._roleManager = roleManager;
                this._report = report;
        }
        [HttpPut("rpt-collection-by-user")]

        public async Task<IActionResult> GetCollectionList([FromHeader] string username)
        {
            var user = await this._userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("Username not found !");
            }

            var data = await (from payment in this._report.Set<Payment>()
                              join collected in this._report.Set<PCollections>() on
                                   new { Casher = payment.Createdby, CollectionID = payment.CollectionID } 
                                    equals 
                                   new { Casher = collected.Casher, CollectionID = collected.CollectionId }
                              into collecton
                              from collectionItem in collecton.DefaultIfEmpty()
                              where payment.Createdby == username
                              group 
                                new {payment, collectionItem } 
                                    by 
                                new {payment.Createdby,payment.HospitalName,payment.Purpose,payment.Type,payment.Channel, payment.CollectionID,payment.IsCollected ,
                                     collectionItem.CollectionId,collectionItem.Casher,collectionItem.CollectedBy,collectionItem.CollectedOn,collectionItem
                                    } into RptData

                              select new
                              {
                                  
                                 Cashier = RptData.Key.Createdby,
                                 Hospital = RptData.Key.HospitalName,
                                 Purpose = RptData.Key.Purpose,
                                 Type = RptData.Key.Type,
                                 Channel = RptData.Key.Channel,
                                 CasherAmount= RptData.Sum(pay=>pay.payment.Amount!=null? pay.payment.Amount:0),
                                 CollectedAmount = RptData.Max(pay => pay.collectionItem.CollectedAmount != null ? pay.collectionItem.CollectedAmount : 0),
                                 StarDate = RptData.Min(date=>date.payment.CreatedOn),
                                 EndDate = RptData.Max(date => date.payment.CreatedOn)

                              }
                              
                              ).ToListAsync();



            return Ok();
        }

    }
}
