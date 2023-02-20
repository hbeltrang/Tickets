using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tickets.Application.Features.Categories.Queries.GetCategoryList;
using Tickets.Application.Features.Categories.Vms;

namespace Tickets.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("list", Name = "GetCategoryList")]
        [ProducesResponseType(typeof(IReadOnlyList<CategoryVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CategoryVm>>> GetCategoryList()
        {
            var query = new GetCategoryListQuery();
            return Ok(await _mediator.Send(query));
        }

    }
}
