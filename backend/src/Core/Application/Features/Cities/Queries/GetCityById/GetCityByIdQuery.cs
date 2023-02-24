using MediatR;
using Tickets.Application.Features.Cities.Vms;

namespace Tickets.Application.Features.Cities.Queries.GetCityById
{
    public class GetCityByIdQuery : IRequest<CityVm>
    {
        public int CityId { get; set; }

        public GetCityByIdQuery(int cityId)
        {
            CityId = cityId == 0 ? throw new ArgumentNullException(nameof(cityId)) : cityId;
        }

    }
}
