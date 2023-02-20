using MediatR;

namespace Tickets.Application.Features.Auths.Roles.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<List<string>>
    {
    }
}
