using GT.AuthService.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GT.AuthService.Domain.Utils
{
    public class EmailHelper
    {
        private readonly string _rootPath;

        public EmailHelper(string rootPath)
        {
            _rootPath = rootPath;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage, EmailSettings _settings)
        {
            using var client = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
            {
                Credentials = new NetworkCredential(_settings.SmtpUser, _settings.SmtpPass),
                EnableSsl = true
            };

            var message = new MailMessage
            {
                From = new MailAddress(_settings.FromEmail, _settings.FromName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(email);

            await client.SendMailAsync(message);
        }
        public string ReadTemplate(string fileName)
        {
            var path = Path.Combine(_rootPath, "Templates", fileName);
            return File.Exists(path) ? File.ReadAllText(path) : string.Empty;
        }

    }
}
