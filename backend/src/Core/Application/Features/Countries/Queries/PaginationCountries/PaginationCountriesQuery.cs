using MediatR;
using Tickets.Application.Features.Countries.Vms;
using Tickets.Application.Features.Shared.Queries;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Countries.Queries.PaginationCountries
{
    public class PaginationCountriesQuery : PaginationBaseQuery, IRequest<PaginationVm<CountryVm>>
    {
        public int? CountryId { get; set; }
        public Status? Status { get; set; }
    }
}
