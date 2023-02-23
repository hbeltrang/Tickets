using Tickets.Domain.Common;

namespace Tickets.Application.Features.States.Vms
{
    public class StateVm
    {
        public int Id { get; set; }        
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? slug { get; set; }        
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public Status? Status { get; set; }
    }
}
