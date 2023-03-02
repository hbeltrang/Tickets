using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tickets.Web.Areas.Admin.ViewModels;
using Tickets.Web.Services;

namespace Tickets.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatesController : Controller
    {
        private readonly IApiService<StateVm> _service;

        public StatesController(IApiService<StateVm> service)
        {
            _service= service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StateVm> vm = new List<StateVm>();
            var endpoint = "api/v1/states";
            var response = await _service.GetAllAsync(endpoint, vm);

            if (response.IsSuccess)
            {
                var result = JsonConvert.DeserializeObject<List<StateVm>>((string)response.Result!);
                vm = result!;
            }

            var newState = new StateVm
            {
                Name = "NL",
                Code = "NL",
                CountryId = 1,
            };


            endpoint = "api/v1/states/create";
            var responseCreate = await _service.CreateAsync(endpoint, newState);


            return View(vm);
        }


        //public ActionResult Create()
        //{
        //    ViewBag.Error = string.Empty;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(StateVm vm)
        //{
        //    ViewBag.Error = string.Empty;
        //    if (ModelState.IsValid)
        //    {
        //        ApiResponse apiResponse = await _service.Create(vm);
        //        if (apiResponse.IsSuccess)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.Error = apiResponse.Message;
        //        }
        //    }

        //    return View(vm);
        //}

        //public async Task<ActionResult> Edit(int id)
        //{
        //    StateVm vm = new StateVm();

        //    if (id == null)
        //    {
        //        return View(vm);
        //    }


        //    ApiResponse apiResponse = await _service.GetById(id);

        //    if (!apiResponse.IsSuccess)
        //    {
        //        return View(vm);
        //    }

        //    vm = (StateVm)apiResponse.Result!;

        //    ViewBag.Error = string.Empty;

        //    return View(vm);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(StateVm vm)
        //{
        //    ViewBag.Error = string.Empty;
        //    if (ModelState.IsValid)
        //    {
        //        ApiResponse apiResponse = await _service.Update(vm);
        //        if (apiResponse.IsSuccess)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.Error = apiResponse.Message;
        //        }
        //    }

        //    return View(vm);
        //}

        //public async Task<ActionResult> Details(int id)
        //{
        //    StateVm vm = new StateVm();

        //    if (id == null)
        //    {
        //        return View(vm);
        //    }


        //    ApiResponse apiResponse = await _service.GetById(id);

        //    if (!apiResponse.IsSuccess)
        //    {
        //        return View(vm);
        //    }

        //    vm = (StateVm)apiResponse.Result!;

        //    return View(vm);
        //}

        //public async Task<ActionResult> Delete(int id)
        //{
        //    StateVm vm = new StateVm();

        //    if (id == null)
        //    {
        //        return View(vm);
        //    }

        //    ApiResponse apiResponse = await _service.GetById(id);

        //    if (!apiResponse.IsSuccess)
        //    {
        //        return View(vm);
        //    }

        //    vm = (StateVm)apiResponse.Result!;

        //    ViewBag.Error = string.Empty;

        //    return View(vm);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    ViewBag.Error = string.Empty;
        //    ApiResponse apiResponse = await _service.GetById(id);
        //    if (apiResponse.IsSuccess)
        //    {
        //        apiResponse = await _service.Delete(id);
        //        if (apiResponse.IsSuccess)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.Error = apiResponse.Message;
        //        }
        //    }

        //    return View();
        //}

    }
}
