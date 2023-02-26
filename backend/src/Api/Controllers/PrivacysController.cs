using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tickets.Application.Features.Privacy.Commands.CreatePrivacy;
using Tickets.Application.Features.Privacy.Commands.DeletePrivacy;
using Tickets.Application.Features.Privacy.Commands.UpdatePrivacy;
using Tickets.Application.Features.Privacy.Queries.GetPrivacyById;
using Tickets.Application.Features.Privacy.Queries.GetPrivacyList;
using Tickets.Application.Features.Privacy.Vms;
using Tickets.Application.Models.Authorization;

namespace Tickets.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PrivacysController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrivacysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet(Name = "GetPrivacys")]
        [ProducesResponseType(typeof(IReadOnlyList<PrivacyVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<PrivacyVm>>> GetPrivacyes()
        {
            var query = new GetPrivacyListQuery();
            var privacys = await _mediator.Send(query);
            return Ok(privacys);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet("{id}", Name = "GetPrivacyById")]
        [ProducesResponseType(typeof(PrivacyVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PrivacyVm>> GetPrivacyById(int id)
        {
            var query = new GetPrivacyByIdQuery(id);
            return Ok(await _mediator.Send(query));

        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPost("create", Name = "CreatePrivacy")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PrivacyVm>> CreatePrivacy([FromBody] CreatePrivacyCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPut("update", Name = "UpdatePrivacy")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PrivacyVm>> UpdatePrivacy([FromBody] UpdatePrivacyCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpDelete("{id}", Name = "DeletePrivacy")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PrivacyVm>> DeletePrivacy(int id)
        {
            var request = new DeletePrivacyCommand(id);
            return await _mediator.Send(request);
        }
    }
}
