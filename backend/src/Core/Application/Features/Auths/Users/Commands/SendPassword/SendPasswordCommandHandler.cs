using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Tickets.Application.Contracts.Infrastructure;
using Tickets.Application.Exceptions;
using Tickets.Application.Messages;
using Tickets.Application.Models.Email;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Commands.SendPassword
{
    public class SendPasswordCommandHandler : IRequestHandler<SendPasswordCommand, string>
    {
        private readonly IEmailService _serviceEmail;
        private readonly UserManager<ApplicationUser> _userManager;

        public SendPasswordCommandHandler(IEmailService serviceEmail, UserManager<ApplicationUser> userManager)
        {
            _serviceEmail = serviceEmail;
            _userManager = userManager;
        }

        public async Task<string> Handle(SendPasswordCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email!);
            if (usuario is null)
            {
                throw new BadRequestException(MessageLabel.UserNotFound);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
            var plainTextBytes = Encoding.UTF8.GetBytes(token);
            token = Convert.ToBase64String(plainTextBytes);

            var emailMessage = new EmailMessage
            {
                To = request.Email,
                Body = MessageLabel.UserResetPasswordEmailBody,
                Subject = MessageLabel.UserResetPasswordEmailSubject
            };

            var result = await _serviceEmail.SendEmail(emailMessage, token);

            if (!result)
            {
                throw new Exception(MessageLabel.UserResetPasswordEmailError);
            }

            return $"{MessageLabel.UserResetPasswordSendEmail} {request.Email}";
        }
    }
}
