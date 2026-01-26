using dev_portfolio.Components.Models;

namespace dev_portfolio.Components.Data;

public interface IEmailService
{
    Task<bool> SendEmail(EmailData emailData);
}