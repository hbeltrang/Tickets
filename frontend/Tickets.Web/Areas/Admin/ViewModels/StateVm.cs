using Tickets.Web.Models;

namespace Tickets.Web.Areas.Admin.ViewModels
{
    public class StateVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Slug { get; set; }        
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public Status? Status { get; set; }
    }
}
