using MediatR;
using Tickets.Application.Features.Cities.Vms;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Cities.Commands.UpdateCity
{
    public class UpdateCityCommand : IRequest<CityVm>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public Status? Status { get; set; }
    }
}
