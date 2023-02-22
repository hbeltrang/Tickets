using MediatR;
using Tickets.Application.Features.Countries.Vms;

namespace Tickets.Application.Features.Countries.Queries.GetCountryById
{
    public class GetCountryByIdQuery : IRequest<CountryVm>
    {
        public int CountryId { get; set; }

        public GetCountryByIdQuery(int countryId)
        {
            CountryId = countryId == 0 ? throw new ArgumentNullException(nameof(countryId)) : countryId;
        }
    }
}
