namespace Tickets.Application.Features.Companies.Vms
{
    public class CompanyImageVm
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? PublicCode { get; set; }
        public int CompanyId { get; set; }
    }
}
