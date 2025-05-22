using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MoH_Microservice.Data;
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
        public ProvidersController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext payment
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._provider = payment;
        }

        [HttpPost("add-provider")]

        public async Task<IActionResult> addProviders([FromBody] ProvidersReg providers)
        {
            var user = await this._userManager.FindByNameAsync(providers.CreatedBy);

            if (user == null)
                return NotFound("User not found");

            try
            {
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
                return BadRequest($"Error: Insert Providers failed! Reason: {ex.StackTrace}");
            }
        }

        [HttpPut("update-provider")]

        public async Task<IActionResult> UpdateProviders([FromBody] ProvidersUpdate providers)
        {
            var user = await this._userManager.FindByNameAsync(providers.UpdatedBy);

            if (user == null)
                return NotFound("User not found");
            try
            {
                var Providers = this._provider.Set<Providers>()
                            .Where(e => e.Id == providers.id)
                            .ExecuteUpdateAsync(item => item.SetProperty(i => i.provider, providers.provider)).Result;
                return Ok(Providers);
            }catch (Exception ex)
            {
                return BadRequest($"Provider Update failed! Reason{ex}");
            }
            
        }
        [HttpDelete("delete-provider")]

        public async Task<IActionResult> DeleteProviders([FromBody] ProvidersDelete providers)
        {
            var user = await this._userManager.FindByNameAsync(providers.DeletedBy);

            if (user == null)
                return NotFound("User not found");

            try
            {
                var Providers = this._provider.Set<Providers>()
                            .Where(e => e.Id == providers.id)
                            .ExecuteDeleteAsync().Result;
                return Ok(Providers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Provider Delete failed! Reason{ex}");
            }
        }

        [HttpGet("list-providers/{username}")]

        public async Task<IActionResult> GetAllProviders([FromRoute] string username)
        {
            var user = await this._userManager.FindByNameAsync(username);

            if (user == null)
                return NotFound("User not found");

            var providers = await this._provider.Set<Providers>().ToArrayAsync();

            return Ok(providers);
        }
    }
}
