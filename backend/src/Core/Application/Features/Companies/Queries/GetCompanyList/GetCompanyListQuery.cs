using MediatR;
using Tickets.Application.Features.Companies.Vms;

namespace Tickets.Application.Features.Companies.Queries.GetCompanyList
{
    public class GetCompanyListQuery : IRequest<IReadOnlyList<CompanyVm>>
    {
    }
}
