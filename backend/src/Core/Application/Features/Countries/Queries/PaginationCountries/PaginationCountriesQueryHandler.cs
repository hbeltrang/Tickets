using AutoMapper;
using MediatR;
using Tickets.Application.Features.Countries.Vms;
using Tickets.Application.Features.Shared.Queries;
using Tickets.Application.Persistence;
using Tickets.Application.Specifications.Countries;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Countries.Queries.PaginationCountries
{
    public class PaginationCountriesQueryHandler : IRequestHandler<PaginationCountriesQuery, PaginationVm<CountryVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationCountriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationVm<CountryVm>> Handle(PaginationCountriesQuery request, CancellationToken cancellationToken)
        {
            var countrySpecificationParams = new CountrySpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
                CountryId = request.CountryId,
            };

            var spec = new CountrySpecification(countrySpecificationParams);
            var countries = await _unitOfWork.Repository<Country>().GetAllWithSpec(spec);
            countries = countries.Where(x => x.Status == Status.Active).ToList();

            var specCount = new CountryForCountingSpecification(countrySpecificationParams);
            var totalCountries = await _unitOfWork.Repository<Country>().CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalCountries) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<CountryVm>>(countries);
            var productsByPage = countries.Count();

            var pagination = new PaginationVm<CountryVm>
            {
                Count = totalCountries,
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
