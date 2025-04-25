using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace MoH_Microservice.Services
{
    public class EmailSender(IConfiguration _configuration) : IEmailSender
    {


        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromAddress = _configuration["EmailSettings:DefaultEmailAddress"];
            var smtpServer = _configuration["EmailSettings:Server"];
            var smtpPort = Convert.ToInt32(_configuration["EmailSettings:Port"]);


            var message = new MailMessage
            {
                From = new MailAddress(fromAddress),                
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email));

            using var client = new SmtpClient(smtpServer, smtpPort);
            //client.Credentials = new NetworkCredential(fromAddress, "Tsedey@123");
            //client.EnableSsl = false;



            try
            {
                await client.SendMailAsync(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"SMTP Error: {ex}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex}");
            }
        }
    }
}
