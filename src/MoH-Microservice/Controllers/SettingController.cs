using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoH_Microservice.Data;
using MoH_Microservice.Misc;
using MoH_Microservice.Models.Database;
using MoH_Microservice.Models.Form;
using MoH_Microservice.Query;
using System.Configuration;
using System.Text.RegularExpressions;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy= "AdminPolicy")]
    public class SettingController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private AppDbContext _dbContext;
        private TokenValidate _tokenValidate;
        
        public SettingController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,AppDbContext dbContext)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._dbContext = dbContext;
            this._tokenValidate = new TokenValidate(userManager);
        }

        [HttpPost("set-permission-group")]
        public async Task<IActionResult> setPermissionGroup([FromBody] GroupSetting groups, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                GroupSetting groupSetting = new GroupSetting
                {
                    Name = groups.Name,
                    description = groups.description,
                    type = groups.type,
                    group = groups.group,
                    isAdmin = groups.isAdmin,
                    IsGrunted = groups.isAdmin,
                    action = groups.action,
                    CreatedBy = user.UserName,

                };

                this._dbContext.AddAsync(groupSetting);
                this._dbContext.SaveChanges();
                return Created("/", new
                {
                    permission = groupSetting
                });
            }
            catch (Exception ex)
            {
                return BadRequest("FAILD TO SET GROUP PERMISSION : ");
            }
        }

        [HttpPost("set-permission-user")]
        public async Task<IActionResult> setPermissionUser([FromBody] UserSetting userP, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                UserSetting userSetting = new UserSetting
                {
                    Name = userP.Name,
                    description = userP.description,
                    type = userP.type,
                    userId = userP.userId,
                    isAdmin = userP.isAdmin,
                    IsGrunted = userP.isAdmin,
                    action = userP.action,
                    CreatedBy = user.UserName,
                };

                this._dbContext.AddAsync(userSetting);
                this._dbContext.SaveChanges();
                return Created("/", new
                {
                    permission = userSetting
                });

            }
            catch (Exception ex)
            {
                return BadRequest("FAILD TO SET USER'S PERMISSION : ");
            }
     
        }

        [HttpPut("update-permission-group")]
        public async Task<IActionResult> updatePermissionGroup([FromBody] GroupSettingUpdate groups, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var update = await this._dbContext.groupSettings.Where(w => w.id == groups.id)
                    .ExecuteUpdateAsync(p => p
                .SetProperty(s => s.IsGrunted, groups.IsGrunted)
                .SetProperty(s => s.group, groups.group)
                .SetProperty(s => s.isAdmin, groups.isAdmin)
                .SetProperty(s => s.UpdatedOn, DateTime.Now)
                .SetProperty(s => s.UpdatedBy, user.UserName)
                );
                return Ok("PERMISSION UPDATED SUCCESSFULY");
            }
            catch (Exception ex)
            {
                return BadRequest("FAILD TO U PDATE GROUP PERMISSION : ");
            }
       
        }

        [HttpPut("update-permission-user")]
        public async Task<IActionResult> updatePermissionUser([FromBody] UserSettingUpdate userP, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var update = await this._dbContext.userSettings.Where(w => w.id == userP.id)
                    .ExecuteUpdateAsync(p => p
                    .SetProperty(s => s.IsGrunted, userP.IsGrunted)
                    .SetProperty(s => s.userId, userP.userId)
                    .SetProperty(s => s.isAdmin, userP.isAdmin)
                    .SetProperty(s => s.UpdatedOn, DateTime.Now)
                    .SetProperty(s => s.UpdatedBy, user.UserName)
                    );

                if (update <= 0)
                {
                    throw new Exception("PERMISSION UPDATE FAILED");
                }
                return Ok("PERMISSION UPDATED SUCCESSFULY");

            }
            catch (Exception ex)
            {
                return BadRequest("FAILD TO UPDATE USER'S PERMISSION : ");
            }
       
        }

        [HttpDelete("delete-permission-group")]
        public async Task<IActionResult> deletePermissionGroup([FromBody] int permissionId, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var update = await this._dbContext.groupSettings.Where(w => w.id==permissionId)
                    .ExecuteDeleteAsync();
                return Ok("PERMISSION UPDATED SUCCESSFULY");
            }
            catch (Exception ex)
            {
                return BadRequest("FAILD TO DELETE GROUP PERMISSION : ");
            }

        }

        [HttpDelete("delete-permission-user")]
        public async Task<IActionResult> deletePermissionUser([FromBody] int permissionId, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var update = await this._dbContext.userSettings.Where(w => w.id == permissionId)
                   .ExecuteDeleteAsync();
                return Ok("PERMISSION DELETED SUCCESSFULY");

            }
            catch (Exception ex)
            {
                return BadRequest("FAILD TO DELETE USER'S PERMISSION : ");
            }
      
        }

        [HttpGet("get-permission-group")]
        public async Task<IActionResult> getPermissionGroup([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var groupSetting = await this._dbContext.groupSettings.ToArrayAsync();
                return Ok(new { data = groupSetting, msg = "SUCCESSFUL!" });
            }
            catch (Exception ex)
            {
                return BadRequest("FAILD TO FETCH GROUP PERMISSION : ");
            }
        }

        [HttpGet("get-permission-user")]
        public async Task<IActionResult> getPermissionUser([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var usersSetting = await this._dbContext.userSettings.ToArrayAsync();
                return Ok(new { data = usersSetting, msg = "SUCCESSFUL!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "FAILD TO FETCH USER'S PERMISSION : " });
            }
        }

        [HttpGet("paymentType-limit")]
        public async Task<IActionResult> gettPaymentLimitUser([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var usersSetting = await this._dbContext.PaymentPurposeLimits.ToArrayAsync();
                return Ok(new { data = usersSetting, msg = "SUCCESSFUL!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "FAILD TO FETCH USER'S PERMISSION : " });
            }
        }

        [HttpGet("paymentType-limit/{paymentPurpose}")]
        public async Task<IActionResult> getOnetPaymentLimitUser([FromRoute] string paymentPurpose,[FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var paymentType = await this._dbContext.PaymentTypes.Where(e => e.type.ToLower() == paymentPurpose.ToLower()).ToArrayAsync();
                if (paymentType.Length <= 0)
                {
                    throw new Exception("Payment type not found");
                }
                var paymentTypeID = paymentType.FirstOrDefault().Id;
                var usersSetting = await this._dbContext.PaymentPurposeLimits.Where(e=>e.type== paymentTypeID).ToArrayAsync();
                return Ok(new { data = usersSetting, msg = "SUCCESSFUL!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "FAILD TO FETCH USER'S PERMISSION : " });
            }
        }

        [HttpDelete("paymentType-limit")]
        public async Task<IActionResult> deletetPaymentLimitUser([FromBody]int id,[FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var usersSetting = await this._dbContext.PaymentPurposeLimits.Where(w=>w.Id==id).ExecuteDeleteAsync();
                return Ok(new { data = usersSetting, msg = "SUCCESSFUL!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "FAILD TO FETCH USER'S PERMISSION : " });
            }
        }
        [HttpPut("paymentType-limit")]
        public async Task<IActionResult> updatetPaymentLimitUser([FromBody] PaymentPurposeLimitReg reg, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var paymentType = await this._dbContext.PaymentTypes.Where(e => e.type.ToLower() == reg.type.ToLower()).ToArrayAsync();
                if (paymentType.Length <= 0)
                {
                    throw new Exception("Payment type not found");
                }
                var paymentTypeID = paymentType.FirstOrDefault().Id;

                var usersSetting = await this._dbContext
                    .PaymentPurposeLimits
                    .Where(w => w.Id == reg.id)
                    .ExecuteUpdateAsync(u => u
                        .SetProperty(p => p.type, paymentTypeID)
                        .SetProperty(p => p.Amount, reg.Amount)
                        .SetProperty(p => p.Time, reg.Time)
                        );
                return Ok(new { data = usersSetting, msg = "SUCCESSFUL!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "FAILD TO FETCH USER'S PERMISSION : " });
            }
           
        }
        // 2025-06-13
        [HttpPost("paymentType-limit")]
        public async Task<IActionResult> settPaymentLimitUser([FromBody] PaymentPurposeLimitReg reg,[FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var paymentType = await this._dbContext.PaymentTypes.Where(e => e.type.ToLower() == reg.type.ToLower()).ToArrayAsync();
                if (paymentType.Length  <= 0)
                {
                    throw new Exception("Payment type not found");
                }
                var paymentTypeID = paymentType.FirstOrDefault().Id;
        
                var payLimitSetting = await this._dbContext.PaymentPurposeLimits
                    .Where(e=>e.type== paymentTypeID)
                    .ToArrayAsync();

                if(payLimitSetting.Length >0)
                {
                    var id = payLimitSetting.FirstOrDefault().Id;
                    reg.id = id;
                    await updatetPaymentLimitUser(reg,Authorization);

                    return Ok(new { data=reg,msg="Found aleardy recored payment type and updated."});
                }
                
                PaymentTypeLimit payLimit = new PaymentTypeLimit
                {
                    type = paymentTypeID,
                    Amount = reg.Amount,
                    Time = reg.Time,
                    CreatedBy = user.UserName,
                    CreatedOn = DateTime.Now
                };

                this._dbContext.Add(payLimit);
                this._dbContext.SaveChanges();

                return Created("/",new {data=reg,msg="Limit set successfully!"});

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"Limit set failed : {ex.Message}" });
            }
            
        }


    }
}
