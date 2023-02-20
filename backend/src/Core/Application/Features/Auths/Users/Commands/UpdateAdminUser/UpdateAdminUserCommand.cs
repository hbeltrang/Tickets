using MediatR;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommand : IRequest<ApplicationUser>
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }

    }
}
