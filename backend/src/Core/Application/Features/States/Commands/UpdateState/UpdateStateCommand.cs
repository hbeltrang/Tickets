using MediatR;
using Tickets.Application.Features.States.Vms;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.States.Commands.UpdateState
{
    public class UpdateStateCommand : IRequest<StateVm>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Slug { get; set; }
        public int? CountryId { get; set; }
        public Status? Status { get; set; }
    }
}
