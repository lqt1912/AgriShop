using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text;
using MailKit.Net.Smtp;
namespace AnBinhMarket.Services.EmailServices
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class EmailService : IEmailService
    {
        MailSettings _mailSettings;
        public IConfiguration configuration { get; set; }

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
            _mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
        }



        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            MimeMessage email = new MimeMessage();

            email.Sender = new MailboxAddress(Encoding.UTF8, _mailSettings.DisplayName, _mailSettings.Mail);

            email.From.Add(email.Sender);

            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            BodyBuilder builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (Microsoft.AspNetCore.Http.IFormFile file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using SmtpClient smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
