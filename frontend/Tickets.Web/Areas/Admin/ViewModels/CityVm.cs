using Tickets.Web.Models;

namespace Tickets.Web.Areas.Admin.ViewModels
{
    public class CityVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public Status? Status { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
    }
}
