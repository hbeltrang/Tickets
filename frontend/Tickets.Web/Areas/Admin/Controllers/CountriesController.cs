using Microsoft.AspNetCore.Mvc;
using Tickets.Web.Areas.Admin.ViewModels;
using Tickets.Web.Models;
using Tickets.Web.Services;

namespace Tickets.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountriesController : Controller
    {
        private readonly ICountryService _service;
        //private readonly IApiService _service;

        public CountriesController(ICountryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CountryVm> vm = new List<CountryVm>();
            ApiResponse apiResponse = await _service.GetAll();

            if (apiResponse.IsSuccess)
            {
                vm = (List<CountryVm>)apiResponse.Result!;
            }
            return View(vm);
        }

        public ActionResult Create()
        {
            ViewBag.Error = string.Empty;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CountryVm vm)
        {
            ViewBag.Error = string.Empty;
            if (ModelState.IsValid)
            {
                ApiResponse apiResponse = await _service.Create(vm);
                if (apiResponse.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = apiResponse.Message;
                }
            }

            return View(vm);
        }

        public async Task<ActionResult> Edit(int id)
        {
            CountryVm vm = new CountryVm();

            if (id == null)
            {
                return View(vm);
            }


            ApiResponse apiResponse = await _service.GetById(id);

            if (!apiResponse.IsSuccess)
            {
                return View(vm);
            }

            vm = (CountryVm)apiResponse.Result!;

            ViewBag.Error = string.Empty;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CountryVm vm)
        {
            ViewBag.Error = string.Empty;
            if (ModelState.IsValid)
            {
                ApiResponse apiResponse = await _service.Update(vm);
                if (apiResponse.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = apiResponse.Message;
                }
            }            

            return View(vm);
        }

        public async Task<ActionResult> Details(int id)
        {
            CountryVm vm = new CountryVm();

            if (id == null)
            {
                return View(vm);
            }


            ApiResponse apiResponse = await _service.GetById(id);

            if (!apiResponse.IsSuccess)
            {
                return View(vm);
            }

            vm = (CountryVm)apiResponse.Result!;

            return View(vm);
        }

        public async Task<ActionResult> Delete(int id)
        {
            CountryVm vm = new CountryVm();

            if (id == null)
            {
                return View(vm);
            }

            ApiResponse apiResponse = await _service.GetById(id);

            if (!apiResponse.IsSuccess)
            {
                return View(vm);
            }

            vm = (CountryVm)apiResponse.Result!;

            ViewBag.Error = string.Empty;

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Error = string.Empty;
            ApiResponse apiResponse = await _service.GetById(id);
            if (apiResponse.IsSuccess)
            {
                apiResponse = await _service.Delete(id);
                if (apiResponse.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = apiResponse.Message;
                }
            }

            return View();
        }
    }
}
