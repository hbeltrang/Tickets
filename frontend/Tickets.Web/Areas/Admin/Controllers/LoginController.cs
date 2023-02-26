using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tickets.Web.Models;
using Tickets.Web.Services;

namespace Tickets.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("admin")]
    //[Route("admin/login")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService= userService;
        }

        //[Route("")]
        //[Route("index")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                //call api user register
                ApiResponse apiResponse = await _userService.Login(model);
                if (apiResponse.IsSuccess)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View(); 
        }

        //[HttpGet]
        //[AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserRegister model)
        {
            if (ModelState.IsValid)
            {
                //call api user register
                ApiResponse apiResponse = await _userService.Register(model);
                if (apiResponse.IsSuccess)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View(model);
        }


    }
}
