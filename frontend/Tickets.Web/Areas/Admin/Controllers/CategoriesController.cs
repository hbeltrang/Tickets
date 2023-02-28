using Microsoft.AspNetCore.Mvc;
using System.Net;
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
            List<CategoryVm> categoriesVm = new List<CategoryVm>();
            ApiResponse apiResponse = await _categoryService.List();
           
            if (apiResponse.IsSuccess)
            {
                categoriesVm = (List<CategoryVm>)apiResponse.Result!;
            }
            return View(categoriesVm);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryVm view)
        {
            if (ModelState.IsValid)
            {
                ApiResponse apiResponse = await _categoryService.Save(view);
                if (apiResponse.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(view);
        }

        public async Task<ActionResult> Edit(int id)
        {
            CategoryVm categoriesVm = new CategoryVm();

            if (id == null)
            {
                return View(categoriesVm);
            }


            ApiResponse apiResponse = await _categoryService.GetById(id);

            if (!apiResponse.IsSuccess)
            {
                return View(categoriesVm);
            }

            categoriesVm = (CategoryVm)apiResponse.Result!;

            return View(categoriesVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryVm view)
        {
            if (ModelState.IsValid)
            {
                ApiResponse apiResponse = await _categoryService.Edit(view);
                if (apiResponse.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(view);
        }

        public async Task<ActionResult> Details(int id)
        {
            CategoryVm categoriesVm = new CategoryVm();

            if (id == null)
            {
                return View(categoriesVm);
            }

            
            ApiResponse apiResponse = await _categoryService.GetById(id);

            if (!apiResponse.IsSuccess)
            {
                return View(categoriesVm);
            }

            categoriesVm = (CategoryVm)apiResponse.Result!;

            return View(categoriesVm);
        }

        public async Task<ActionResult> Delete(int id)
        {
            CategoryVm categoriesVm = new CategoryVm();

            if (id == null)
            {
                return View(categoriesVm);
            }

            ApiResponse apiResponse = await _categoryService.GetById(id);

            if (!apiResponse.IsSuccess)
            {
                return View(categoriesVm);
            }

            categoriesVm = (CategoryVm)apiResponse.Result!;

            return View(categoriesVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ApiResponse apiResponse = await _categoryService.GetById(id);
            if (apiResponse.IsSuccess)
            {
                apiResponse = await _categoryService.Delete(id);
                if (apiResponse.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

    }
}
