using MediatR;
using Tickets.Application.Features.States.Vms;

namespace Tickets.Application.Features.States.Commands.DeleteState
{
    public class DeleteStateCommand: IRequest<StateVm>
    {
        public int StateId { get; set; }

        public DeleteStateCommand(int stateId)
        {
            StateId = stateId == 0 ? throw new ArgumentException(nameof(stateId)) : stateId;
        }
    }
}
