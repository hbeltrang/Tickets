using Microsoft.AspNetCore.Mvc;
using Tickets.Web.Services;

namespace Tickets.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CitiesController : Controller
    {
        private readonly ICountryService _service;

        public CitiesController(ICountryService service)
        {
            _service = service;
        }


    }
}
