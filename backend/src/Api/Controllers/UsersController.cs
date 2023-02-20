﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tickets.Application.Contracts.Infrastructure;
using Tickets.Application.Features.Auths.Roles.Queries.GetRoles;
using Tickets.Application.Features.Auths.Users.Commands.LoginUser;
using Tickets.Application.Features.Auths.Users.Commands.RegisterUser;
using Tickets.Application.Features.Auths.Users.Commands.ResetPassword;
using Tickets.Application.Features.Auths.Users.Commands.ResetPasswordByToken;
using Tickets.Application.Features.Auths.Users.Commands.SendPassword;
using Tickets.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser;
using Tickets.Application.Features.Auths.Users.Commands.UpdateAdminUser;
using Tickets.Application.Features.Auths.Users.Commands.UpdateUser;
using Tickets.Application.Features.Auths.Users.Queries.GetUserById;
using Tickets.Application.Features.Auths.Users.Queries.GetUserByToken;
using Tickets.Application.Features.Auths.Users.Queries.GetUserByUsername;
using Tickets.Application.Features.Auths.Users.Queries.PaginationUsers;
using Tickets.Application.Features.Auths.Users.Vms;
using Tickets.Application.Features.Shared.Queries;
using Tickets.Application.Models.Authorization;
using Tickets.Application.Models.ImageManagement;
using Tickets.Domain;

namespace Tickets.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private IMediator _mediator;
        private IManageImageService _manageImageService;

        public UsersController(IMediator mediator, IManageImageService manageImageService)
        {
            _mediator = mediator;
            _manageImageService = manageImageService;
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "Login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpPost("register", Name = "Register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Register([FromForm] RegisterUserCommand request)
        {
            if (request.Photo is not null)
            {
                var resultImage = await _manageImageService.UploadImage(new ImageData
                {
                    ImageStream = request.Photo!.OpenReadStream(),
                    Name = request.Photo.Name
                }
                );

                request.PhotoId = resultImage.PublicId;
                request.PhotoUrl = resultImage.Url;
            }

            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpPost("forgotpassword", Name = "ForgotPassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] SendPasswordCommand request)
        {
            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpPost("resetpassword", Name = "ResetPassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordByTokenCommand request)
        {
            return await _mediator.Send(request);
        }


        [HttpPost("updatepassword", Name = "UpdatePassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdatePassword([FromBody] ResetPasswordCommand request)
        {
            return await _mediator.Send(request);
        }


        [HttpPut("update", Name = "Update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Update([FromForm] UpdateUserCommand request)
        {

            if (request.Photo is not null)
            {
                var resultImage = await _manageImageService.UploadImage(new ImageData
                {
                    ImageStream = request.Photo!.OpenReadStream(),
                    Name = request.Photo!.Name
                }
                );

                request.PhotoId = resultImage.PublicId;
                request.PhotoUrl = resultImage.Url;
            }


            return await _mediator.Send(request);
        }



        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("updateAdminUser", Name = "UpdateAdminUser")]
        [ProducesResponseType(typeof(ApplicationUser), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApplicationUser>> UpdateAdminUser([FromBody] UpdateAdminUserCommand request)
        {
            return await _mediator.Send(request);
        }


        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("updateAdminStastusUser", Name = "UpdateAdminStastusUser")]
        [ProducesResponseType(typeof(ApplicationUser), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApplicationUser>> UpdateAdminStastusUser([FromBody] UpdateAdminStatusUserCommand request)
        {
            return await _mediator.Send(request);
        }


        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> GetUserById(string id)
        {
            var query = new GetUserByIdQuery(id);
            return await _mediator.Send(query);
        }



        [HttpGet("", Name = "CurrentUser")]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> CurrentUser()
        {
            var query = new GetUserByTokenQuery();
            return await _mediator.Send(query);
        }


        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("username/{username}", Name = "GetUserByUsername")]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> GetUserByUsername(string username)
        {
            var query = new GetUserByUsernameQuery(username);
            return await _mediator.Send(query);
        }


        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("paginationUser", Name = "PaginationUser")]
        [ProducesResponseType(typeof(PaginationVm<ApplicationUser>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationVm<ApplicationUser>>> PaginationUser(
                        [FromQuery] PaginationUsersQuery paginationUsersQuery
                    )
        {
            var paginationUser = await _mediator.Send(paginationUsersQuery);
            return Ok(paginationUser);
        }


        [AllowAnonymous]
        [HttpGet("roles", Name = "GetRolesList")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<string>>> GetRolesList()
        {
            var query = new GetRolesQuery();
            return Ok(await _mediator.Send(query));

        }

    }
}
