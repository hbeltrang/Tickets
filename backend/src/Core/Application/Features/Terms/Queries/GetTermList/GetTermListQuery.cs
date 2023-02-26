using MediatR;
using Tickets.Application.Features.Terms.Vms;

namespace Tickets.Application.Features.Terms.Queries.GetTermList
{
    public class GetTermListQuery : IRequest<IReadOnlyList<TermVm>>
    {

    }
}
