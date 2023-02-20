using MediatR;
using Microsoft.AspNetCore.Identity;
using Tickets.Application.Contracts.Identity;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Auths.Users.Vms;
using Tickets.Application.Messages;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Auths.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(
                            UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            IAuthService authService
                            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existeUserByEmail = await _userManager.FindByEmailAsync(request.Email!) is null ? false : true;
            if (existeUserByEmail)
            {
                throw new BadRequestException(MessageLabel.UserAlreadyExists);
            }

            var existeUserByUsername = await _userManager.FindByNameAsync(request.Username!) is null ? false : true;
            if (existeUserByEmail)
            {
                throw new BadRequestException(MessageLabel.UserAlreadyExists);
            }

            var user = new ApplicationUser
            {
                Name = request.Name,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email,
                UserName = request.Username,
                AvatarUrl = request.PhotoUrl
            };

            var resultado = await _userManager.CreateAsync(user!, request.Password!);

            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, AppRole.User);
                var roles = await _userManager.GetRolesAsync(user);

                return new AuthResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Email = user.Email,
                    Username = user.UserName,
                    Avatar = user.AvatarUrl,
                    Token = _authService.CreateToken(user, roles),
                    Roles = roles
                };

            }

            throw new Exception(MessageLabel.UserRegisterError);

        }
    }
    
}
