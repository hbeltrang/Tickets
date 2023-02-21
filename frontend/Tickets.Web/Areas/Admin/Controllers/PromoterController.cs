using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tickets.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin,Promoter")]
    public class PromoterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
