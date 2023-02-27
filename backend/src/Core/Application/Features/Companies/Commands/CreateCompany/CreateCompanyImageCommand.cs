namespace Tickets.Application.Features.Companies.Commands.CreateCompany
{
    public class CreateCompanyImageCommand
    {
        public string? ImageUrl { get; set; }
        public string? PublicCode { get; set; }
        public int CompanyId { get; set; }
    }
}
