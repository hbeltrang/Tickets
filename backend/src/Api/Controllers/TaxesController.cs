using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tickets.Application.Features.Taxes.Commands.CreateTax;
using Tickets.Application.Features.Taxes.Commands.DeleteTax;
using Tickets.Application.Features.Taxes.Commands.UpdateTax;
using Tickets.Application.Features.Taxes.Querys.GetTaxById;
using Tickets.Application.Features.Taxes.Querys.GetTaxList;
using Tickets.Application.Features.Taxes.Vms;
using Tickets.Application.Models.Authorization;

namespace Tickets.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TaxesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaxesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet(Name = "GetTaxes")]
        [ProducesResponseType(typeof(IReadOnlyList<TaxVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<TaxVm>>> GetTaxes()
        {
            var query = new GetTaxListQuery();
            var taxes = await _mediator.Send(query);
            return Ok(taxes);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet("{id}", Name = "GetTaxById")]
        [ProducesResponseType(typeof(TaxVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TaxVm>> GetTaxById(int id)
        {
            var query = new GetTaxByIdQuery(id);
            return Ok(await _mediator.Send(query));

        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPost("create", Name = "CreateTax")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<TaxVm>> CreateTax([FromBody] CreateTaxCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPut("update", Name = "UpdateTax")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<TaxVm>> UpdateTax([FromBody] UpdateTaxCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpDelete("{id}", Name = "DeleteTax")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<TaxVm>> DeleteTax(int id)
        {
            var request = new DeleteTaxCommand(id);
            return await _mediator.Send(request);
        }

    }
}
