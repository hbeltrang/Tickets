using MediatR;
using Tickets.Application.Features.Companies.Vms;

namespace Tickets.Application.Features.Companies.Queries.GetCompanyById
{
    public class GetCompanyByIdQuery : IRequest<CompanyVm>
    {
        public int CompanyId { get; set; }

        public GetCompanyByIdQuery(int companyId)
        {
            CompanyId = companyId == 0 ? throw new ArgumentNullException(nameof(companyId)) : companyId;
        }

    }
}
