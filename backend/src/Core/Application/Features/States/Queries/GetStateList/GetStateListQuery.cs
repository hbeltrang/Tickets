using MediatR;
using Tickets.Application.Features.States.Vms;

namespace Tickets.Application.Features.States.Queries.GetStateList
{
    public class GetStateListQuery : IRequest<IReadOnlyList<StateVm>>
    {

    }
}
