using MediatR;
using Tickets.Application.Features.Countries.Vms;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommand : IRequest<CountryVm>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Code { get; set; }
        public Status? Status { get; set; }
    }
}
