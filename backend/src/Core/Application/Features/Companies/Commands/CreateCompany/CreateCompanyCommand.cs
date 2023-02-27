using MediatR;
using Microsoft.AspNetCore.Http;
using Tickets.Application.Features.Companies.Vms;

namespace Tickets.Application.Features.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommand : IRequest<CompanyVm>
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? CellPhone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? MapUrl { get; set; }
        public IReadOnlyList<IFormFile>? Photos { get; set; }
        public IReadOnlyList<CreateCompanyImageCommand>? ImageUrls { get; set; }
    }
}
