using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tickets.Application.Features.Countries.Queries.GetCountryList;
using Tickets.Application.Features.Countries.Vms;

namespace Tickets.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CountriesController : ControllerBase
    {
        private IMediator _mediator;

        public CountriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetCountries")]
        [ProducesResponseType(typeof(IReadOnlyList<CountryVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CountryVm>>> GetCountries()
        {
            var query = new GetCountryListQuery();
            return Ok(await _mediator.Send(query));
        }

    }
}
