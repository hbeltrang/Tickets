using MediatR;
using Tickets.Application.Features.Auths.Users.Vms;

namespace Tickets.Application.Features.Auths.Users.Queries.GetUserByUsername
{
    public class GetUserByUsernameQuery : IRequest<AuthResponse>
    {
        public string? Username { get; set; }

        public GetUserByUsernameQuery(string username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(Username));
        }

    }
}
