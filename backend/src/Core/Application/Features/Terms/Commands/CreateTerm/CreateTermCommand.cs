using MediatR;
using Tickets.Application.Features.Terms.Vms;

namespace Tickets.Application.Features.Terms.Commands.CreateTerm
{
    public class CreateTermCommand : IRequest<TermVm>
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
    }
}
