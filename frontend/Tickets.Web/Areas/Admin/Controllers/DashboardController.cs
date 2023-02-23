using Microsoft.AspNetCore.Mvc;

namespace Tickets.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/dashboard")]
    public class DashboardController : Controller
    {
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
