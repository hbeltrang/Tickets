using MediatR;
using Tickets.Application.Features.Shared.Queries;
using Tickets.Application.Persistence;
using Tickets.Application.Specifications.Users;
using Tickets.Domain;

namespace Tickets.Application.Features.Auths.Users.Queries.PaginationUsers
{
    public class PaginationUsersQueryHandler : IRequestHandler<PaginationUsersQuery, PaginationVm<ApplicationUser>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaginationUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationVm<ApplicationUser>> Handle(PaginationUsersQuery request, CancellationToken cancellationToken)
        {
            var userSpecificationParams = new UserSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort
            };

            var spec = new UserSpecification(userSpecificationParams);
            var users = await _unitOfWork.Repository<ApplicationUser>().GetAllWithSpec(spec);

            var specCount = new UserForCountingSpecification(userSpecificationParams);
            var totalUsers = await _unitOfWork.Repository<ApplicationUser>().CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalUsers) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var usersByPage = users.Count();

            var pagination = new PaginationVm<ApplicationUser>
            {
                Count = totalUsers,
                Data = users,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = usersByPage
            };

            return pagination;
        }
    }
}
