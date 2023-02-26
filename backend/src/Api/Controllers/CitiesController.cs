using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tickets.Application.Features.Cities.Commands.CreateCity;
using Tickets.Application.Features.Cities.Commands.DeleteCity;
using Tickets.Application.Features.Cities.Commands.UpdateCity;
using Tickets.Application.Features.Cities.Queries.GetCityByCountryStateId;
using Tickets.Application.Features.Cities.Queries.GetCityById;
using Tickets.Application.Features.Cities.Queries.GetCityList;
using Tickets.Application.Features.Cities.Queries.PaginationCities;
using Tickets.Application.Features.Cities.Vms;
using Tickets.Application.Features.Shared.Queries;
using Tickets.Application.Models.Authorization;
using Tickets.Domain.Common;

namespace Tickets.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet(Name = "GetCities")]
        [ProducesResponseType(typeof(IReadOnlyList<CityVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CityVm>>> GetCities()
        {
            var query = new GetCityListQuery();
            var cities = await _mediator.Send(query);
            return Ok(cities);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet("{id}", Name = "GetCityById")]
        [ProducesResponseType(typeof(CityVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CityVm>> GetCityById(int id)
        {
            var query = new GetCityByIdQuery(id);
            return Ok(await _mediator.Send(query));

        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet("country/{countryid}/state/{stateid}", Name = "GetCityByCountryStateId")]
        [ProducesResponseType(typeof(IReadOnlyList<CityVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CityVm>>> GetCityByCountryStateId(int countryid, int stateid)
        {
            var query = new GetCityByCountryStateIdQuery(countryid, stateid);
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPost("create", Name = "CreateCity")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CityVm>> CreateCity([FromBody] CreateCityCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPut("update", Name = "UpdateCity")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CityVm>> UpdateCity([FromBody] UpdateCityCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpDelete("{id}", Name = "DeleteCity")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CityVm>> DeleteCity(int id)
        {
            var request = new DeleteCityCommand(id);
            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpGet("pagination", Name = "PaginationCities")]
        [ProducesResponseType(typeof(PaginationVm<CityVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationVm<CityVm>>> PaginationProduct([FromQuery] PaginationCitiesQuery paginationCitiesQuery)
        {
            paginationCitiesQuery.Status = Status.Active;
            var paginationCities = await _mediator.Send(paginationCitiesQuery);
            return Ok(paginationCities);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet("paginationAdmin", Name = "PaginationCitiesAdmin")]
        [ProducesResponseType(typeof(PaginationVm<CityVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationVm<CityVm>>> PaginationAdmin([FromQuery] PaginationCitiesQuery paginationCitiesQuery)
        {
            var paginationCities = await _mediator.Send(paginationCitiesQuery);
            return Ok(paginationCities);
        }


    }
}
