using MediatR;
using Tickets.Application.Features.Terms.Vms;

namespace Tickets.Application.Features.Terms.Commands.DeleteTerm
{
    public class DeleteTermCommand : IRequest<TermVm>
    {
        public int TermId { get; set; }

        public DeleteTermCommand(int termId)
        {
            TermId = termId == 0 ? throw new ArgumentException(nameof(termId)) : termId;
        }
    }
}
