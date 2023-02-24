using MediatR;
using Tickets.Application.Features.Cities.Vms;

namespace Tickets.Application.Features.Cities.Queries.GetCityList
{
    public class GetCityListQuery : IRequest<IReadOnlyList<CityVm>>
    {
    }
}
