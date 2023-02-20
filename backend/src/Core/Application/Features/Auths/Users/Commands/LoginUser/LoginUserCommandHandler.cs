using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tickets.Application.Contracts.Identity;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Auths.Users.Vms;
using Tickets.Application.Messages;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _sigInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LoginUserCommandHandler(
                            UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> sigInManager,
                            RoleManager<IdentityRole> roleManager,
                            IAuthService authService,
                            IMapper mapper,
                            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
            _roleManager = roleManager;
            _authService = authService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email!);
            if (user is null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.Email!);
            }

            if (!user.IsActive)
            {
                throw new Exception(MessageLabel.UserNotBlocked);
            }

            var resultado = await _sigInManager.CheckPasswordSignInAsync(user, request.Password!, false);

            if (!resultado.Succeeded)
            {
                throw new Exception(MessageLabel.UserCredentialsError);
            }

            var roles = await _userManager.GetRolesAsync(user);

            var authResponse = new AuthResponse
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

            return authResponse;
        }
    }
}
