using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using dev_portfolio.Components.Models;

namespace dev_portfolio.Components.Data;

public class EmailService(IOptions<SmtpSettings> options) : IEmailService
{
    private readonly SmtpSettings _settings = options.Value;

    public bool SendEmail(EmailData emailData)
    {
        try
        {
            var sender = new MailboxAddress(_settings.UserName, "hello@demomailtrap.co");
            var recipient = new MailboxAddress(emailData.RecipientName, emailData.RecipientId);
            var emailBody = new BodyBuilder
            {
                TextBody = emailData.Body
            };
            var emailMessage = new MimeMessage
            {
                Subject = emailData.Subject,
                Body = emailBody.ToMessageBody()
            };
            emailMessage.From.Add(sender);
            emailMessage.To.Add(recipient);

            var mailClient = new SmtpClient();
            mailClient.Connect(_settings.Host, _settings.Port, _settings.UseSSL);
            mailClient.Authenticate(_settings.UserName, _settings.Password);
            mailClient.Send(emailMessage);
            mailClient.Disconnect(true);
            mailClient.Dispose();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
}