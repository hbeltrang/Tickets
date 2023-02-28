using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Tickets.Web.Controllers;
using Tickets.Web.Services;

namespace Tickets.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("admin/dashboard")]
    public class DashboardController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;

        public DashboardController(ILogger<HomeController> logger, LanguageService localization)
        {
            _logger = logger;
            _localization = localization;
        }

        //[Route("")]
        //[Route("index")]
        public IActionResult Index()
        {
            ViewBag.Home = _localization.Getkey("Home").Value;
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;

            return View();
        }

        public IActionResult ChangeLanguage(string newCulture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(newCulture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            //return Redirect(Request.Headers["Referer"].ToString());
            return LocalRedirect(returnUrl);
        }

    }
}
