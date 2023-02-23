using MediatR;
using Tickets.Application.Features.States.Vms;

namespace Tickets.Application.Features.States.Queries.GetStateByCountryId
{
    public class GetStateByCountryIdQuery : IRequest<IReadOnlyList<StateVm>>
    {
        public int CountryId { get; set; }

        public GetStateByCountryIdQuery(int countryId)
        {
            CountryId = countryId == 0 ? throw new ArgumentNullException(nameof(countryId)) : countryId;
        }
    }
}
