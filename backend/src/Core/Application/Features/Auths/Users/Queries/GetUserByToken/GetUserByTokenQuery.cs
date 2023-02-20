using MediatR;
using Tickets.Application.Features.Auths.Users.Vms;

namespace Tickets.Application.Features.Auths.Users.Queries.GetUserByToken
{
    public class GetUserByTokenQuery : IRequest<AuthResponse>
    {

    }
}
