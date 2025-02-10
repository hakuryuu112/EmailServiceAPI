using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using EmailServiceAPI.Models;
using Microsoft.Extensions.Options;

namespace EmailServiceAPI.Controllers.Email
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly DBContext.DatabaseContext _context;
        private readonly SMTPSettingModel _smtpSettings;

        public EmailController(DBContext.DatabaseContext context, IOptions<SMTPSettingModel> smtpSettings)
        {
            _context = context;
            _smtpSettings = smtpSettings.Value;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailSendRequest request)
        {
            try
            {
                var smtpClient = new SmtpClient(_smtpSettings.Server)
                {
                    Port = _smtpSettings.Port,
                    Credentials = new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.SenderPassword),
                    EnableSsl = _smtpSettings.EnableSSL,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.SenderEmail),
                    Subject = request.Subject,
                    Body = $"<html><body><h3>{request.Body}</h3></body></html>",
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(request.Recipient);

                mailMessage.ReplyToList.Add(new MailAddress(_smtpSettings.SenderEmail));

                await smtpClient.SendMailAsync(mailMessage);

                var emailLog = new EmailLogModel
                {
                    Recipient = request.Recipient,
                    Subject = request.Subject,
                    Body = request.Body,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "Admin",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "Admin"
                };

                _context.EmailLogs.Add(emailLog);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Email sent successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Email sending failed", error = ex.Message });
            }
        }

        [HttpGet("logs")]
        public IActionResult GetEmailLogs()
        {
            var logs = _context.EmailLogs.ToList();
            return Ok(logs);
        }
    }
}
