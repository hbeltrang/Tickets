using Microsoft.AspNetCore.Mvc;
using Tickets.Web.Areas.Admin.ViewModels;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ApiResponse apiResponse = await _categoryService.List();
            //List<CategoryVm> categories = new List<CategoryVm>();

            //if (apiResponse.IsSuccess)
            //{
            //    categories = (List<CategoryVm>)apiResponse.Result!;
            //}

            var list = (List<CategoryVm>)apiResponse.Result!;
            //var result = JsonConvert.DeserializeObject<List<CategoryVm>>(list.ToList());

            return View(list);
        }
    }
}
