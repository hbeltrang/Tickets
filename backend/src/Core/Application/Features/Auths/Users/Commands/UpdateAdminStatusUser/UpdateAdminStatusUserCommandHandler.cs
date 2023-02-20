using MediatR;
using Microsoft.AspNetCore.Identity;
using Tickets.Application.Exceptions;
using Tickets.Application.Messages;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser
{
    public class UpdateAdminStatusUserCommandHandler : IRequestHandler<UpdateAdminStatusUserCommand, ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateAdminStatusUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> Handle(UpdateAdminStatusUserCommand request, CancellationToken cancellationToken)
        {
            var updateUser = await _userManager.FindByIdAsync(request.Id!);
            if (updateUser is null)
            {
                throw new BadRequestException(MessageLabel.UserNotFound);
            }

            updateUser.IsActive = !updateUser.IsActive;

            var result = await _userManager.UpdateAsync(updateUser);

            if (!result.Succeeded)
            {
                throw new Exception(MessageLabel.UserChangeStatusError);
            }

            return updateUser;
        }
    }
}
