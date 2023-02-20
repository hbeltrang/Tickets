using MediatR;
using Tickets.Application.Features.Shared.Queries;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Queries.PaginationUsers
{
    public class PaginationUsersQuery : PaginationBaseQuery, IRequest<PaginationVm<ApplicationUser>>
    {
    }
}
