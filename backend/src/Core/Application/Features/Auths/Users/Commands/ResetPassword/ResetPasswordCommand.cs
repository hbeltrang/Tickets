using MediatR;

namespace Tickets.Application.Features.Auths.Users.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public string? NewPassword { get; set; }

        public string? OldPassword { get; set; }
    }
}
