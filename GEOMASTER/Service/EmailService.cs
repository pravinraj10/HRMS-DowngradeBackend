using GEOMASTER.Configurations;
using GEOMASTER.Interface.Email;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace GEOMASTER.Service.Email
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtp;

        public EmailService(
            IOptions<SmtpSettings> smtpSettings)
        {
            _smtp = smtpSettings.Value;
        }

        public async Task SendEmailAsync(
            string to,
            string subject,
            string body)
        {
            var smtp = new SmtpClient(_smtp.Host)
            {
                Port = _smtp.Port,

                Credentials = new NetworkCredential(
                    _smtp.Email,
                    _smtp.Password
                ),

                EnableSsl = _smtp.EnableSsl,
            };

            var message = new MailMessage
            {
                From = new MailAddress(_smtp.Email),

                Subject = subject,

                Body = body,

                IsBodyHtml = true
            };

            message.To.Add(to);

            await smtp.SendMailAsync(message);
        }
    }
}