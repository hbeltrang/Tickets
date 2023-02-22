using MediatR;
using Tickets.Application.Features.Countries.Vms;

namespace Tickets.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommand : IRequest<CountryVm>
    {
        public string? Name { get; set; }
        public string? Iso2 { get; set; }
        public string? Iso3 { get; set; }
    }
}
