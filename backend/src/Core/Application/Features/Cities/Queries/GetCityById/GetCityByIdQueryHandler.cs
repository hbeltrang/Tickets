using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.Cities.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Cities.Queries.GetCityById
{
    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, CityVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCityByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CityVm> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {

            var includes = new List<Expression<Func<City, object>>>();
            includes.Add(p => p.State!);
            includes.Add(p => p.State!.Country!);

            var cities = await _unitOfWork.Repository<City>().GetEntityAsync(
                x => x.Id == request.CityId && x.Status == Status.Active,
                includes,
                false
            );

            return _mapper.Map<CityVm>(cities);
        }
    }
}
