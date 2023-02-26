using MediatR;
using Tickets.Application.Features.Privacy.Vms;

namespace Tickets.Application.Features.Privacy.Commands.CreatePrivacy
{
    public class CreatePrivacyCommand : IRequest<PrivacyVm>
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
    }
}
