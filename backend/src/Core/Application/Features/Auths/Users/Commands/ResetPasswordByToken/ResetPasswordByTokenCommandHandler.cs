using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Tickets.Application.Exceptions;
using Tickets.Application.Messages;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Commands.ResetPasswordByToken
{
    public class ResetPasswordByTokenCommandHandler : IRequestHandler<ResetPasswordByTokenCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordByTokenCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(ResetPasswordByTokenCommand request, CancellationToken cancellationToken)
        {

            if (!string.Equals(request.Password, request.ConfirmPassword))
            {
                throw new BadRequestException(MessageLabel.UserPasswordConfirmDifferent);
            }

            var updateUser = await _userManager.FindByEmailAsync(request.Email!);
            if (updateUser is null)
            {
                throw new BadRequestException(MessageLabel.UserEmailNotFound);
            }

            var token = Convert.FromBase64String(request.Token!);
            var tokenResult = Encoding.UTF8.GetString(token);

            var resetResultado = await _userManager.ResetPasswordAsync(updateUser, tokenResult, request.Password!);
            if (!resetResultado.Succeeded)
            {
                throw new Exception(MessageLabel.UserResetPasswordError);
            }


            return $"{MessageLabel.UserPasswordSuccess}: {request.Email}";

        }
    }
}
