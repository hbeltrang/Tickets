using Tickets.Application.Models.Email;

namespace Tickets.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailMessage email, string token);
    }
}
