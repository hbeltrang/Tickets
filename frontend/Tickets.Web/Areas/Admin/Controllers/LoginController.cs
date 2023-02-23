using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tickets.Web.Areas.Admin.ViewModels;
using Tickets.Web.Models;
using Tickets.Web.Services;

namespace Tickets.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/login")]
    [Route("admin")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService= userService;
        }

        [Route("")]
        [Route("index")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                //call api user register
                ApiResponse apiResponse = await _userService.Login(model);
                if (apiResponse.IsSuccess)
                {
                    return RedirectToAction("index", "Dashboard");
                }
            }
            return View(model);
        }


        [Route("register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserRegister model)
        {
            if (ModelState.IsValid)
            {
                //call api user register
                ApiResponse apiResponse = await _userService.Register(model);
                if (apiResponse.IsSuccess)
                {
                    return RedirectToAction("index", "Dashboard");
                }
            }
            return View(model);
        }


    }
}
