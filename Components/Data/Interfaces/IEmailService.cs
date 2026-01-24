using dev_portfolio.Components.Models;

namespace dev_portfolio.Components.Data;

public interface IEmailService
{
    bool SendEmail(EmailData emailData);
}