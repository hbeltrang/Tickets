using MediatR;
using Tickets.Application.Features.Cities.Vms;
using Tickets.Application.Features.Shared.Queries;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Cities.Queries.PaginationCities
{
    public class PaginationCitiesQuery : PaginationBaseQuery, IRequest<PaginationVm<CityVm>>
    {
        public int? StateId { get; set; }
        public Status? Status { get; set; }
    }
}
