using AutoMapper;
using MediatR;
using Tickets.Application.Features.Cities.Vms;
using Tickets.Application.Features.Shared.Queries;
using Tickets.Application.Persistence;
using Tickets.Application.Specifications.Cities;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Cities.Queries.PaginationCities
{
    public class PaginationCitiesQueryHandler : IRequestHandler<PaginationCitiesQuery, PaginationVm<CityVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationCitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationVm<CityVm>> Handle(PaginationCitiesQuery request, CancellationToken cancellationToken)
        {
            var citySpecificationParams = new CitySpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
                CountryId = request.CountryId,
            };

            var spec = new CitySpecification(citySpecificationParams);
            var cities = await _unitOfWork.Repository<City>().GetAllWithSpec(spec);
            cities = cities.Where(x => x.Status == Status.Active).ToList();

            var specCount = new CityForCountingSpecification(citySpecificationParams);
            var totalCities = await _unitOfWork.Repository<City>().CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalCities) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<CityVm>>(cities);
            var productsByPage = cities.Count();

            var pagination = new PaginationVm<CityVm>
            {
                Count = totalCities,
                Data = data,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = productsByPage
            };

            return pagination;
        }
    }
}
