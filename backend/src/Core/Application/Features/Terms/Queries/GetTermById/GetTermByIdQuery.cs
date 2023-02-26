using MediatR;
using Tickets.Application.Features.Terms.Vms;

namespace Tickets.Application.Features.Terms.Queries.GetTermById
{
    public class GetTermByIdQuery : IRequest<TermVm>
    {
        public int TermId { get; set; }

        public GetTermByIdQuery(int termId)
        {
            TermId = termId == 0 ? throw new ArgumentNullException(nameof(termId)) : termId;
        }

    }

}
