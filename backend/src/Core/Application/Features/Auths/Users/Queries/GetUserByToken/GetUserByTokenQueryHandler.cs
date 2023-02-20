using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tickets.Application.Contracts.Identity;
using Tickets.Application.Features.Auths.Users.Vms;
using Tickets.Application.Messages;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Queries.GetUserByToken
{
    public class GetUserByTokenQueryHandler : IRequestHandler<GetUserByTokenQuery, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByTokenQueryHandler(
            UserManager<ApplicationUser> userManager,
            IAuthService authService,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _authService = authService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_authService.GetSessionUser());
            if (user is null)
            {
                throw new Exception(MessageLabel.UserNotAutenticated);
            }

            if (!user.IsActive)
            {
                throw new Exception(MessageLabel.UserNotBlocked);
            }

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
    }
}
