using MediatR;
using Tickets.Application.Features.Shared.Queries;
using Tickets.Application.Features.States.Vms;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.States.Queries.PaginationStates
{
    public class PaginationStatesQuery : PaginationBaseQuery, IRequest<PaginationVm<StateVm>>
    {
        public int? CountryId { get; set; }
        public Status? Status { get; set; }
    }
}
