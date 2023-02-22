using MediatR;
using Tickets.Application.Features.Countries.Vms;

namespace Tickets.Application.Features.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommand : IRequest<CountryVm>
    {
        public int CountryId { get; set; }

        public DeleteCountryCommand(int countryId)
        {
            CountryId = countryId == 0 ? throw new ArgumentException(nameof(countryId)) : countryId;
        }

    }
}
