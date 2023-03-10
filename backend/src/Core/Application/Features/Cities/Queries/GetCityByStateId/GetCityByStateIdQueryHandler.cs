using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.Cities.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Cities.Queries.GetCityByStateId
{
    public class GetCityByStateIdQueryHandler : IRequestHandler<GetCityByStateIdQuery, IReadOnlyList<CityVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCityByStateIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CityVm>> Handle(GetCityByStateIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<City, object>>>();
            includes.Add(p => p.State!);
            includes.Add(p => p.State!.Country!);

            var cities = await _unitOfWork.Repository<City>().GetAsync(
               x => x.Status == Status.Active && x.StateId == request.StateId,
               x => x.OrderBy(y => y.Name),
               includes,
               false
           );

            var citiesVm = _mapper.Map<IReadOnlyList<CityVm>>(cities);

            return citiesVm;
        }
    }
}
