using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tickets.Application.Features.Terms.Commands.CreateTerm;
using Tickets.Application.Features.Terms.Commands.DeleteTerm;
using Tickets.Application.Features.Terms.Commands.UpdateTerm;
using Tickets.Application.Features.Terms.Queries.GetTermById;
using Tickets.Application.Features.Terms.Queries.GetTermList;
using Tickets.Application.Features.Terms.Vms;
using Tickets.Application.Models.Authorization;

namespace Tickets.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TermsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TermsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet(Name = "GetTerms")]
        [ProducesResponseType(typeof(IReadOnlyList<TermVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<TermVm>>> GetTerms()
        {
            var query = new GetTermListQuery();
            var terms = await _mediator.Send(query);
            return Ok(terms);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet("{id}", Name = "GetTermById")]
        [ProducesResponseType(typeof(TermVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TermVm>> GetTermById(int id)
        {
            var query = new GetTermByIdQuery(id);
            return Ok(await _mediator.Send(query));

        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPost("create", Name = "CreateTerm")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<TermVm>> CreateTerm([FromBody] CreateTermCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPut("update", Name = "UpdateTerm")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<TermVm>> UpdateTerm([FromBody] UpdateTermCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpDelete("{id}", Name = "DeleteTerm")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<TermVm>> DeleteTerm(int id)
        {
            var request = new DeleteTermCommand(id);
            return await _mediator.Send(request);
        }


    }
}
