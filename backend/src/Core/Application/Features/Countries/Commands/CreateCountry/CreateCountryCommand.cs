using MediatR;
using Tickets.Application.Features.Countries.Vms;

namespace Tickets.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommand : IRequest<CountryVm>
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
