using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using dev_portfolio.Components.Models;

namespace dev_portfolio.Components.Data;

public class EmailService(IOptions<SmtpSettings> options) : IEmailService
{
    private readonly SmtpSettings _settings = options.Value;

    public async Task<bool> SendEmail(EmailData emailData)
    {
        var mailClient = new SmtpClient();

        try
        {
            var sender = new MailboxAddress(_settings.UserName, _settings.EmailId);
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

            await mailClient.ConnectAsync(_settings.Host, _settings.Port, _settings.UseSSL);
            await mailClient.AuthenticateAsync(_settings.UserName, _settings.Password);
            await mailClient.SendAsync(emailMessage);
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
        finally
        {
            if (mailClient.IsConnected)
                await mailClient.DisconnectAsync(true);

            mailClient.Dispose();
        }
    }
}