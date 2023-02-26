namespace Tickets.Application.Features.Socials.Commands.CreateSocial
{
    public class CreateSocialImageCommand
    {
        public string? ImageUrl { get; set; }
        public string? PublicCode { get; set; }
        public int SocialId { get; set; }        
    }
}
