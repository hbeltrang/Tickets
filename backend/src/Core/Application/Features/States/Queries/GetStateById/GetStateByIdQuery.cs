using MediatR;
using Tickets.Application.Features.States.Vms;

namespace Tickets.Application.Features.States.Queries.GetStateById
{
    public class GetStateByIdQuery : IRequest<StateVm>
    {
        public int StateId { get; set; }

        public GetStateByIdQuery(int stateId)
        {
            StateId = stateId == 0 ? throw new ArgumentNullException(nameof(stateId)) : stateId;
        }
    }
}
