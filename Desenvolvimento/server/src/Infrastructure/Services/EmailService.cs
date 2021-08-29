using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public async void Send(string titulo, string messagem, string[] emails, string fileName = null, byte[] bit = null)
        {
            try
            {
                SmtpClient client = new SmtpClient
                {
                    Host = _emailConfiguration.SmtpServer,
                    Port = _emailConfiguration.SmtpPort,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword)
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("noreply@SCA.com.br")
                };

                foreach (var email in emails)
                {
                    mailMessage.To.Add(email);
                }

                mailMessage.IsBodyHtml = true; 
                mailMessage.Body = messagem;
                mailMessage.Subject = titulo;

                if (bit != null)
                {
                    Stream file = new MemoryStream(bit);
                    Attachment data = new Attachment(file, fileName);
                    mailMessage.Attachments.Add(data);
                }

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possivel enviar o email.");
            }
        }
    }
}
