using MediatR;
using Microsoft.AspNetCore.Http;
using Tickets.Application.Features.Socials.Commands.CreateSocial;
using Tickets.Application.Features.Socials.Vms;

namespace Tickets.Application.Features.Socials.Commands.UpdateSocial
{
    public class UpdateSocialCommand: IRequest<SocialVm>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PageUrl { get; set; }
        public IReadOnlyList<IFormFile>? Photos { get; set; }
        public IReadOnlyList<CreateSocialImageCommand>? ImageUrls { get; set; }
    }
}
