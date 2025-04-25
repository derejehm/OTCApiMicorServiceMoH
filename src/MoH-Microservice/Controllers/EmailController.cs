using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net.Mail;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest model)
        {
            foreach (var email in model.To)
            {
                await _emailSender.SendEmailAsync(email, model.Subject, model.Body);
            }

            return Ok("Emails sent successfully.");
        }


    }
}
public class EmailRequest
{
    public List<string> To { get; set; }  // Accepts multiple email addresses
    public string Subject { get; set; }
    public string Body { get; set; }
}