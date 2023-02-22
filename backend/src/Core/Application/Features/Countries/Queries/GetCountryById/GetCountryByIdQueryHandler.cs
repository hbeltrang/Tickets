using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.Countries.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Countries.Queries.GetCountryById
{
    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, CountryVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCountryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CountryVm> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Country, object>>>();

            var taxes = await _unitOfWork.Repository<Country>().GetEntityAsync(
                x => x.Id == request.CountryId && x.Status == Status.Active,
                includes,
                false
            );

            return _mapper.Map<CountryVm>(taxes);
        }
    }
}
