using MediatR;
using Tickets.Application.Features.Cities.Vms;

namespace Tickets.Application.Features.Cities.Queries.GetCityByCountryStateId
{
    public class GetCityByCountryStateIdQuery : IRequest<IReadOnlyList<CityVm>>
    {
        public int CountryId { get; set; }
        public int StateId { get; set; }

        public GetCityByCountryStateIdQuery(int countryId, int stateId)
        {
            CountryId = countryId == 0 ? throw new ArgumentNullException(nameof(countryId)) : countryId;
            StateId = stateId == 0 ? throw new ArgumentNullException(nameof(stateId)) : stateId;
        }

    }
}
