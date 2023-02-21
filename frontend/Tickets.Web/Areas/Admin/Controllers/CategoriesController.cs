using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using Tickets.Web.Models;
using Tickets.Web.Services;

namespace Tickets.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService service) 
        {
            _categoryService = service;
        }

        public async Task<IActionResult> Index()
        {
            ApiResponse apiResponse = await _categoryService.List();

            return View(apiResponse);
        }
    }
}
