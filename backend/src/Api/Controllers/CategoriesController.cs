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
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetCategories")]
        [ProducesResponseType(typeof(IReadOnlyList<CategoryVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CategoryVm>>> GetCategories()
        {
            var query = new GetCategoryListQuery();
            var categories = await _mediator.Send(query);
            return Ok(categories);
        }

    }
}
