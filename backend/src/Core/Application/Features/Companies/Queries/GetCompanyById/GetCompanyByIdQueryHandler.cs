using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.Companies.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Companies.Queries.GetCompanyById
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCompanyByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CompanyVm> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Company, object>>>();
            includes.Add(p => p.CompanyImages!);

            var companies = await _unitOfWork.Repository<Company>().GetEntityAsync(
                x => x.Id == request.CompanyId && x.Status == Status.Active,
                includes,
                true
            );

            return _mapper.Map<CompanyVm>(companies);
        }
    }
}
