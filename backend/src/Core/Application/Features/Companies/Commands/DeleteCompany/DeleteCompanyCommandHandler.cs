using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Companies.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Companies.Commands.DeleteCompany
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, CompanyVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCompanyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CompanyVm> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyToUpdate = await _unitOfWork.Repository<Company>().GetByIdAsync(request.CompanyId);
            if (companyToUpdate is null)
            {
                throw new NotFoundException(nameof(Company), request.CompanyId);
            }

            companyToUpdate.Status = companyToUpdate.Status == Status.Inactive
                            ? Status.Active : Status.Inactive;


            await _unitOfWork.Repository<Company>().UpdateAsync(companyToUpdate);

            return _mapper.Map<CompanyVm>(companyToUpdate);
        }
    }
}
