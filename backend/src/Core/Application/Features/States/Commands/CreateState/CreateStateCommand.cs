using MediatR;
using Tickets.Application.Features.States.Vms;

namespace Tickets.Application.Features.States.Commands.CreateState
{
    public class CreateStateCommand: IRequest<StateVm>
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Slug { get; set; }
        public int? CountryId { get; set; }
    }
}
