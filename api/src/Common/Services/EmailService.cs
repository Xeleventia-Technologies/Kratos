using System.Net;
using System.Net.Mail;
using Kratos.Api.Common.Options;
using Microsoft.Extensions.Options;

namespace Kratos.Api.Common.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}

public class EmailService(IOptions<EmailOptions> emailOptions) : IEmailService
{
    private readonly EmailOptions emailOptions = emailOptions.Value;

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using (var client = new SmtpClient(emailOptions.SmtpHost, emailOptions.SmtpPort))
        {
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(emailOptions.Username, emailOptions.Password);

            using (var mail = new MailMessage(emailOptions.Username, to))
            {
                mail.IsBodyHtml = true;
                mail.Subject = subject;
                mail.Body = body;

                await client.SendMailAsync(mail);
            };
        };
    }
}
