using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tickets.Application.Features.Shared.Queries;
using Tickets.Application.Features.States.Commands.CreateState;
using Tickets.Application.Features.States.Commands.DeleteState;
using Tickets.Application.Features.States.Commands.UpdateState;
using Tickets.Application.Features.States.Queries.GetStateByCountryId;
using Tickets.Application.Features.States.Queries.GetStateById;
using Tickets.Application.Features.States.Queries.GetStateList;
using Tickets.Application.Features.States.Queries.PaginationStates;
using Tickets.Application.Features.States.Vms;
using Tickets.Application.Models.Authorization;
using Tickets.Domain.Common;

namespace Tickets.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetStates")]
        [ProducesResponseType(typeof(IReadOnlyList<StateVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<StateVm>>> GetStates()
        {
            var query = new GetStateListQuery();
            var states = await _mediator.Send(query);
            return Ok(states);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetStateById")]
        [ProducesResponseType(typeof(StateVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<StateVm>> GetStateById(int id)
        {
            var query = new GetStateByIdQuery(id);
            return Ok(await _mediator.Send(query));

        }

        [AllowAnonymous]
        [HttpGet("country/{id}", Name = "GetStateByCountryId")]
        [ProducesResponseType(typeof(IReadOnlyList<StateVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<StateVm>>> GetStateByCountryId(int id)
        {
            var query = new GetStateByCountryIdQuery(id);
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPost("create", Name = "CreateState")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<StateVm>> CreateState([FromBody] CreateStateCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPut("update", Name = "UpdateState")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<StateVm>> UpdateState([FromBody] UpdateStateCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpDelete("{id}", Name = "DeleteState")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<StateVm>> DeleteState(int id)
        {
            var request = new DeleteStateCommand(id);
            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpGet("pagination", Name = "PaginationStates")]
        [ProducesResponseType(typeof(PaginationVm<StateVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationVm<StateVm>>> PaginationProduct([FromQuery] PaginationStatesQuery paginationStatesQuery)
        {
            paginationStatesQuery.Status = Status.Active;
            var paginationStates = await _mediator.Send(paginationStatesQuery);
            return Ok(paginationStates);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet("paginationAdmin", Name = "PaginationStatesAdmin")]
        [ProducesResponseType(typeof(PaginationVm<StateVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationVm<StateVm>>> PaginationAdmin([FromQuery] PaginationStatesQuery paginationStatesQuery)
        {
            var paginationStates = await _mediator.Send(paginationStatesQuery);
            return Ok(paginationStates);
        }


    }
}
