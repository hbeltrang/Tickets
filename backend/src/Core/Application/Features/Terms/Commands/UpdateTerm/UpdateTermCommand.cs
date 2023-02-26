using MediatR;
using Tickets.Application.Features.Terms.Vms;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Terms.Commands.UpdateTerm
{
    public class UpdateTermCommand : IRequest<TermVm>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public Status? Status { get; set; }
    }
}
