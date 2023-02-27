using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tickets.Application.Contracts.Infrastructure;
using Tickets.Application.Features.Socials.Commands.CreateSocial;
using Tickets.Application.Features.Socials.Commands.DeleteSocial;
using Tickets.Application.Features.Socials.Commands.UpdateSocial;
using Tickets.Application.Features.Socials.Queries.GetSocialById;
using Tickets.Application.Features.Socials.Queries.GetSocialList;
using Tickets.Application.Features.Socials.Vms;
using Tickets.Application.Models.Authorization;
using Tickets.Application.Models.ImageManagement;

namespace Tickets.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SocialsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IManageImageService _manageImageService;

        public SocialsController(IMediator mediator, IManageImageService manageImageService)
        {
            _mediator = mediator;
            _manageImageService = manageImageService;
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet(Name = "GetSocials")]
        [ProducesResponseType(typeof(IReadOnlyList<SocialVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<SocialVm>>> GetSocials()
        {
            var query = new GetSocialListQuery();
            var socials = await _mediator.Send(query);
            return Ok(socials);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet("{id}", Name = "GetSocialById")]
        [ProducesResponseType(typeof(SocialVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SocialVm>> GetSocialById(int id)
        {
            var query = new GetSocialByIdQuery(id);
            return Ok(await _mediator.Send(query));

        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPost("create", Name = "CreateSocial")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<SocialVm>> CreateSocial([FromForm] CreateSocialCommand request)
        {
            var listFotoUrls = new List<CreateSocialImageCommand>();

            if (request.Photos is not null)
            {
                foreach (var photo in request.Photos)
                {
                    var resultImage = await _manageImageService.UploadImage(new ImageData
                    {
                        ImageStream = photo.OpenReadStream(),
                        Name = photo.Name
                    });

                    var photoCommand = new CreateSocialImageCommand
                    {
                        PublicCode = resultImage.PublicId,
                        ImageUrl = resultImage.Url
                    };

                    listFotoUrls.Add(photoCommand);
                }
                request.ImageUrls = listFotoUrls;
            }

            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPut("update", Name = "UpdateSocial")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<SocialVm>> UpdateSocial([FromForm] UpdateSocialCommand request)
        {
            var listFotoUrls = new List<CreateSocialImageCommand>();

            if (request.Photos is not null)
            {
                foreach (var photo in request.Photos)
                {
                    var resultImage = await _manageImageService.UploadImage(new ImageData
                    {
                        ImageStream = photo.OpenReadStream(),
                        Name = photo.Name
                    });

                    var photoCommand = new CreateSocialImageCommand
                    {
                        PublicCode = resultImage.PublicId,
                        ImageUrl = resultImage.Url
                    };

                    listFotoUrls.Add(photoCommand);
                }
                request.ImageUrls = listFotoUrls;
            }

            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpDelete("{id}", Name = "DeleteSocial")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<SocialVm>> DeleteSocial(int id)
        {
            var request = new DeleteSocialCommand(id);
            return await _mediator.Send(request);
        }


    }
}
