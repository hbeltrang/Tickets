using MediatR;
using Microsoft.AspNetCore.Identity;
using Tickets.Application.Features.Auths.Users.Vms;
using Tickets.Application.Messages;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserByIdQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId!);
            if (user is null)
            {
                throw new Exception(MessageLabel.UserNotFound);
            }

            return new AuthResponse
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                Username = user.UserName,
                Avatar = user.AvatarUrl,
                Roles = await _userManager.GetRolesAsync(user)
            };

        }
    }
}
