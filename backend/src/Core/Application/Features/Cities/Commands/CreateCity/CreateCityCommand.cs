using MediatR;
using Tickets.Application.Features.Cities.Vms;

namespace Tickets.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommand : IRequest<CityVm>
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
    }
}
