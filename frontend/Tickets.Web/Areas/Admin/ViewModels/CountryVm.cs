using Tickets.Web.Models;

namespace Tickets.Web.Areas.Admin.ViewModels
{
    public class CountryVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public Status? Status { get; set; }
    }
}
