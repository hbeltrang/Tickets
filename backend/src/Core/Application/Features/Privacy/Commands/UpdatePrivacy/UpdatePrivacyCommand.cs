using MediatR;
using Tickets.Application.Features.Privacy.Vms;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Privacy.Commands.UpdatePrivacy
{
    public class UpdatePrivacyCommand : IRequest<PrivacyVm>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public Status? Status { get; set; }
    }
}
