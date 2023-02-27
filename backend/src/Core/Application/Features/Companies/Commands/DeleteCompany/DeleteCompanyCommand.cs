using MediatR;
using Tickets.Application.Features.Companies.Vms;

namespace Tickets.Application.Features.Companies.Commands.DeleteCompany
{
    public class DeleteCompanyCommand : IRequest<CompanyVm>
    {
        public int CompanyId { get; set; }

        public DeleteCompanyCommand(int companyId)
        {
            CompanyId = companyId == 0 ? throw new ArgumentException(nameof(companyId)) : companyId;
        }

    }
}
