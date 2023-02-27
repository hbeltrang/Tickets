using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tickets.Application.Features.Promoters.Commands.CreatePromoter;
using Tickets.Application.Features.Promoters.Commands.DeletePromoter;
using Tickets.Application.Features.Promoters.Commands.UpdatePromoter;
using Tickets.Application.Features.Promoters.Queries.GetPromoterById;
using Tickets.Application.Features.Promoters.Queries.GetPromoterList;
using Tickets.Application.Features.Promoters.Vms;
using Tickets.Application.Models.Authorization;

namespace Tickets.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromotersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PromotersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet(Name = "GetPromoters")]
        [ProducesResponseType(typeof(IReadOnlyList<PromoterVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<PromoterVm>>> GetPromoters()
        {
            var query = new GetPromoterListQuery();
            var promoters = await _mediator.Send(query);
            return Ok(promoters);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet("{id}", Name = "GetPromoterById")]
        [ProducesResponseType(typeof(PromoterVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PromoterVm>> GetPromoterById(int id)
        {
            var query = new GetPromoterByIdQuery(id);
            return Ok(await _mediator.Send(query));

        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPost("create", Name = "CreatePromoter")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PromoterVm>> CreatePromoter([FromBody] CreatePromoterCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPut("update", Name = "UpdatePromoter")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PromoterVm>> UpdatePromoter([FromBody] UpdatePromoterCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpDelete("{id}", Name = "DeletePromoter")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PromoterVm>> DeletePromoter(int id)
        {
            var request = new DeletePromoterCommand(id);
            return await _mediator.Send(request);
        }


    }
}
