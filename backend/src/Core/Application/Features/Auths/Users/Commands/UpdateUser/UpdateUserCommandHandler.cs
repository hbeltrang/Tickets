using MediatR;
using Microsoft.AspNetCore.Identity;
using Tickets.Application.Contracts.Identity;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Auths.Users.Vms;
using Tickets.Application.Messages;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public UpdateUserCommandHandler(
                        UserManager<ApplicationUser> userManager,
                        RoleManager<IdentityRole> roleManager,
                        IAuthService authService
                        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var updateUsuario = await _userManager.FindByNameAsync(_authService.GetSessionUser());
            if (updateUsuario is null)
            {
                throw new BadRequestException(MessageLabel.UserNotFound);
            }

            updateUsuario.Name = request.Name;
            updateUsuario.LastName = request.LastName;
            updateUsuario.Phone = request.Phone;
            updateUsuario.AvatarUrl = request.PhotoUrl ?? updateUsuario.AvatarUrl;

            var resultado = await _userManager.UpdateAsync(updateUsuario);

            if (!resultado.Succeeded)
            {
                throw new Exception(MessageLabel.UserUpdateError);
            }

            var userById = await _userManager.FindByEmailAsync(request.Email!);
            var roles = await _userManager.GetRolesAsync(userById!);

            return new AuthResponse
            {
                Id = userById!.Id,
                Name = userById.Name,
                LastName = userById.LastName,
                Phone = userById.Phone,
                Email = userById.Email,
                Username = userById.UserName,
                Avatar = userById.AvatarUrl,
                Token = _authService.CreateToken(userById, roles),
                Roles = roles
            };


        }
    }
}
