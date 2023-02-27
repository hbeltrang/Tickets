using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.Companies.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Companies.Queries.GetCompanyList
{
    public class GetCompanyListQueryHandler : IRequestHandler<GetCompanyListQuery, IReadOnlyList<CompanyVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCompanyListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CompanyVm>> Handle(GetCompanyListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Company, object>>>();
            includes.Add(p => p.CompanyImages!);

            var companies = await _unitOfWork.Repository<Company>().GetAsync(
                x => x.Status == Status.Active,
                x => x.OrderBy(y => y.Name),
                includes,
                false
            );

            return _mapper.Map<IReadOnlyList<CompanyVm>>(companies);
        }
    }
}
