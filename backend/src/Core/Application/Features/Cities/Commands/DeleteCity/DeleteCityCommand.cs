using MediatR;
using Tickets.Application.Features.Cities.Vms;

namespace Tickets.Application.Features.Cities.Commands.DeleteCity
{
    public class DeleteCityCommand : IRequest<CityVm>
    {
        public int CityId { get; set; }

        public DeleteCityCommand(int cityId)
        {
            CityId = cityId == 0 ? throw new ArgumentException(nameof(cityId)) : cityId;
        }

    }
}
