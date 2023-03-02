using MediatR;
using Tickets.Application.Features.Cities.Vms;

namespace Tickets.Application.Features.Cities.Queries.GetCityByStateId
{
    public class GetCityByStateIdQuery : IRequest<IReadOnlyList<CityVm>>
    {
        public int StateId { get; set; }

        public GetCityByStateIdQuery(int stateId)
        {
            StateId = stateId == 0 ? throw new ArgumentNullException(nameof(stateId)) : stateId;
        }

    }
}
