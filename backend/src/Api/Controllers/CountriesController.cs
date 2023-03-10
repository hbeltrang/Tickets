using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tickets.Application.Features.Countries.Commands.CreateCountry;
using Tickets.Application.Features.Countries.Commands.DeleteCountry;
using Tickets.Application.Features.Countries.Commands.UpdateCountry;
using Tickets.Application.Features.Countries.Queries.GetCountryById;
using Tickets.Application.Features.Countries.Queries.GetCountryList;
using Tickets.Application.Features.Countries.Queries.PaginationCountries;
using Tickets.Application.Features.Countries.Vms;
using Tickets.Application.Features.Shared.Queries;
using Tickets.Application.Models.Authorization;
using Tickets.Domain.Common;

namespace Tickets.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet(Name = "GetCountries")]
        [ProducesResponseType(typeof(IReadOnlyList<CountryVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CountryVm>>> GetCountries()
        {
            var query = new GetCountryListQuery();
            var countries = await _mediator.Send(query);
            return Ok(countries);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet("{id}", Name = "GetCountryById")]
        [ProducesResponseType(typeof(CountryVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CountryVm>> GetCountryById(int id)
        {
            var query = new GetCountryByIdQuery(id);
            return Ok(await _mediator.Send(query));

        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPost("create", Name = "CreateCountry")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CountryVm>> CreateCountry([FromBody] CreateCountryCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPut("update", Name = "UpdateCountry")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CountryVm>> UpdateCountry([FromBody] UpdateCountryCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpDelete("{id}", Name = "DeleteCountry")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CountryVm>> DeleteCountry(int id)
        {
            var request = new DeleteCountryCommand(id);
            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpGet("pagination", Name = "PaginationCountries")]
        [ProducesResponseType(typeof(PaginationVm<CountryVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationVm<CountryVm>>> PaginationProduct([FromQuery] PaginationCountriesQuery paginationCountriesQuery)
        {
            paginationCountriesQuery.Status = Status.Active;
            var paginationCountries = await _mediator.Send(paginationCountriesQuery);
            return Ok(paginationCountries);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet("paginationAdmin", Name = "PaginationCountriesAdmin")]
        [ProducesResponseType(typeof(PaginationVm<CountryVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationVm<CountryVm>>> PaginationAdmin([FromQuery] PaginationCountriesQuery paginationCountriesQuery)
        {
            var paginationCountries = await _mediator.Send(paginationCountriesQuery);
            return Ok(paginationCountries);
        }


    }
}
