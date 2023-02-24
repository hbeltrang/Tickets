using Tickets.Domain.Common;

namespace Tickets.Application.Features.Cities.Vms
{
    public class CityVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public int StateId { get; set; }
        public string? StateName { get; set; }
        public Status? Status { get; set; }
    }
}
