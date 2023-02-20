using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Application.Features.Auths.Users.Vms;
using Tickets.Application.Messages;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Queries.GetUserByUsername
{
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserByUsernameQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username!);

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
