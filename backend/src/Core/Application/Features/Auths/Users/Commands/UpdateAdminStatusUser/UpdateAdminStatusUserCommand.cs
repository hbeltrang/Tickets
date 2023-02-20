using MediatR;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser
{
    public class UpdateAdminStatusUserCommand : IRequest<ApplicationUser>
    {
        public string? Id { get; set; }
    }
}
