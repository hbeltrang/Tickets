using Tickets.Web.Models;

namespace Tickets.Web.Areas.Admin.ViewModels
{
    public class CategoryVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Status? Status { get; set; }
    }
}
