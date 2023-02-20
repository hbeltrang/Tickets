using MediatR;
using Microsoft.AspNetCore.Identity;
using Tickets.Application.Contracts.Identity;
using Tickets.Application.Exceptions;
using Tickets.Application.Messages;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandHandler : IRequestHandler<UpdateAdminUserCommand, ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public UpdateAdminUserCommandHandler(
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IAuthService authService
                )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<ApplicationUser> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
        {

            var updateUser = await _userManager.FindByIdAsync(request.Id!);
            if (updateUser is null)
            {
                throw new BadRequestException(MessageLabel.UserNotFound);
            }

            updateUser.Name = request.Name;
            updateUser.LastName = request.LastName;
            updateUser.Phone = request.Phone;

            var resultado = await _userManager.UpdateAsync(updateUser);

            if (!resultado.Succeeded)
            {
                throw new Exception(MessageLabel.UserUpdateError);
            }

            var role = await _roleManager.FindByNameAsync(request.Role!);
            if (role is null)
            {
                throw new Exception(MessageLabel.UserAssignRoleError);
            }

            await _userManager.AddToRoleAsync(updateUser, role.Name!);

            return updateUser;
        }
    }
}
