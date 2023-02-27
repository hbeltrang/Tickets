using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tickets.Application.Contracts.Infrastructure;
using Tickets.Application.Features.Companies.Commands.CreateCompany;
using Tickets.Application.Features.Companies.Commands.DeleteCompany;
using Tickets.Application.Features.Companies.Commands.UpdateCompany;
using Tickets.Application.Features.Companies.Queries.GetCompanyById;
using Tickets.Application.Features.Companies.Queries.GetCompanyList;
using Tickets.Application.Features.Companies.Vms;
using Tickets.Application.Models.Authorization;
using Tickets.Application.Models.ImageManagement;

namespace Tickets.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IManageImageService _manageImageService;

        public CompaniesController(IMediator mediator, IManageImageService manageImageService)
        {
            _mediator = mediator;
            _manageImageService = manageImageService;
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet(Name = "GetCompanies")]
        [ProducesResponseType(typeof(IReadOnlyList<CompanyVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CompanyVm>>> GetCompanies()
        {
            var query = new GetCompanyListQuery();
            var companies = await _mediator.Send(query);
            return Ok(companies);
        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpGet("{id}", Name = "GetCompanyById")]
        [ProducesResponseType(typeof(CompanyVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyVm>> GetCompanyById(int id)
        {
            var query = new GetCompanyByIdQuery(id);
            return Ok(await _mediator.Send(query));

        }

        [Authorize(Roles = Role.ADMINOrAPIADMIN)]
        [HttpPost("create", Name = "CreateCompany")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyVm>> CreateCompany([FromForm] CreateCompanyCommand request)
        {
            var listFotoUrls = new List<CreateCompanyImageCommand>();

            if (request.Photos is not null)
            {
                foreach (var photo in request.Photos)
                {
                    var resultImage = await _manageImageService.UploadImage(new ImageData
                    {
                        ImageStream = photo.OpenReadStream(),
                        Name = photo.Name
                    });

                    var photoCommand = new CreateCompanyImageCommand
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
        [HttpPut("update", Name = "UpdateCompany")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyVm>> UpdateCompany([FromForm] UpdateCompanyCommand request)
        {
            var listFotoUrls = new List<CreateCompanyImageCommand>();

            if (request.Photos is not null)
            {
                foreach (var photo in request.Photos)
                {
                    var resultImage = await _manageImageService.UploadImage(new ImageData
                    {
                        ImageStream = photo.OpenReadStream(),
                        Name = photo.Name
                    });

                    var photoCommand = new CreateCompanyImageCommand
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
        [HttpDelete("{id}", Name = "DeleteCompany")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyVm>> DeleteCompany(int id)
        {
            var request = new DeleteCompanyCommand(id);
            return await _mediator.Send(request);
        }


    }
}
