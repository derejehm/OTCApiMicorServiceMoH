using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MoH_Microservice.Data;
using MoH_Microservice.Misc;
using MoH_Microservice.Models;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProvidersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private AppDbContext _provider;
        private TokenValidate _tokenValidate;
        public ProvidersController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext payment
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._provider = payment;
            this._tokenValidate = new TokenValidate(userManager);
        }

        [HttpPost("add-provider")]

        public async Task<IActionResult> addProviders([FromBody] ProvidersReg providers, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                Providers provider = new Providers
                {
                    provider = providers.provider,
                    service = providers.service,
                    Createdby = user.UserName,
                    CreatedOn = DateTime.Now,
                    Updatedby = null,
                    UpdateOn = null,
                };
                await this._provider.AddAsync<Providers>(provider);
                await this._provider.SaveChangesAsync();

                return Created("/", provider);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg= $"Error: Insert Providers failed! Reason: {ex.Message}" });
            }
        }

        [HttpPut("update-provider")]

        public async Task<IActionResult> UpdateProviders([FromBody] ProvidersUpdate providers, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var Providers = this._provider.Providers
                            .Where(e => e.Id == providers.id)
                            .ExecuteUpdateAsync(item => item.SetProperty(i => i.provider, providers.provider)).Result;
                return Ok(Providers);
            }catch (Exception ex)
            {
                return BadRequest(new { msg = $"Provider Update failed! Reason {ex.Message}" });
            }
            
        }
        [HttpDelete("delete-provider")]
        public async Task<IActionResult> DeleteProviders([FromBody] ProvidersDelete providers, [FromHeader] string Authorization)
        {

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var Providers = this._provider.Providers
                            .Where(e => e.Id == providers.id)
                            .ExecuteDeleteAsync().Result;
                return Ok(Providers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"Provider Delete failed! Reason: {ex.Message}" });
            }
        }

        [HttpGet("list-providers")]

        public async Task<IActionResult> GetAllProviders([FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var providers = await this._provider.Providers.ToArrayAsync();
                return Ok(providers);
            }
            catch (Exception ex)
            {
                return BadRequest(new {msg=ex.Message});
            }
        }
    }
}
