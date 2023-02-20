using MediatR;
using Microsoft.AspNetCore.Identity;
using Tickets.Application.Contracts.Identity;
using Tickets.Application.Exceptions;
using Tickets.Application.Messages;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;

        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var updateUsuario = await _userManager.FindByNameAsync(_authService.GetSessionUser());
            if (updateUsuario is null)
            {
                throw new BadRequestException(MessageLabel.UserNotFound);
            }

            var resultValidateOldPassword = _userManager.PasswordHasher
                 .VerifyHashedPassword(updateUsuario, updateUsuario.PasswordHash!, request.OldPassword!);

            if (!(resultValidateOldPassword == PasswordVerificationResult.Success))
            {
                throw new BadRequestException(MessageLabel.UserWrongPassword);
            }

            var hashedNewPassword = _userManager.PasswordHasher.HashPassword(updateUsuario, request.NewPassword!);
            updateUsuario.PasswordHash = hashedNewPassword;

            var result = await _userManager.UpdateAsync(updateUsuario);

            if (!result.Succeeded)
            {
                throw new Exception(MessageLabel.UserResetPasswordError);
            }

            return result.Succeeded;
        }
    }
}
