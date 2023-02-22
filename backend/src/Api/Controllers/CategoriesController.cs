using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tickets.Application.Features.Categories.Commands.CreateCategory;
using Tickets.Application.Features.Categories.Commands.DeleteCategory;
using Tickets.Application.Features.Categories.Commands.UpdateCategory;
using Tickets.Application.Features.Categories.Queries.GetCategoryById;
using Tickets.Application.Features.Categories.Queries.GetCategoryList;
using Tickets.Application.Features.Categories.Vms;
using Tickets.Application.Models.Authorization;

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

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetCategoryById")]
        [ProducesResponseType(typeof(CategoryVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryVm>> GetCategoryById(int id)
        {
            var query = new GetCategoryByIdQuery(id);
            return Ok(await _mediator.Send(query));

        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPost("create", Name = "CreateCategory")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryVm>> CreateCategory([FromBody] CreateCategoryCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPut("update", Name = "UpdateCategory")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryVm>> UpdateCategory([FromBody] UpdateCategoryCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpDelete("{id}", Name = "DeleteCategory")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryVm>> DeleteCategory(int id)
        {
            var request = new DeleteCategoryCommand(id);
            return await _mediator.Send(request);
        }

    }
}
