using MediatR;
using Tickets.Application.Features.Countries.Vms;

namespace Tickets.Application.Features.Countries.Queries.GetCountryList
{
    public class GetCountryListQuery : IRequest<IReadOnlyList<CountryVm>>
    {
    }
}
