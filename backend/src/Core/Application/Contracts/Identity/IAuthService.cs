using Tickets.Domain;

namespace Tickets.Application.Contracts.Identity
{
    public interface IAuthService
    {
        string GetSessionUser();
        string CreateToken(ApplicationUser user, IList<string>? roles);

    }
}
