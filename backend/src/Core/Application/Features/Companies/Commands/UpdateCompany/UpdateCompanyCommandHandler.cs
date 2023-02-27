using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Companies.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCompanyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CompanyVm> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyToUpdate = await _unitOfWork.Repository<Company>().GetByIdAsync(request.Id);
            if (companyToUpdate is null)
            {
                throw new NotFoundException(nameof(Company), request.Id);
            }

            _mapper.Map(request, companyToUpdate, typeof(UpdateCompanyCommand), typeof(Company));
            await _unitOfWork.Repository<Company>().UpdateAsync(companyToUpdate);

            if ((request.ImageUrls is not null) && request.ImageUrls.Count > 0)
            {
                var imagesToRemove = await _unitOfWork.Repository<CompanyImage>().GetAsync(
                    x => x.CompanyId == request.Id
                );

                _unitOfWork.Repository<CompanyImage>().DeleteRange(imagesToRemove);

                request.ImageUrls.Select(c => { c.CompanyId = request.Id; return c; }).ToList();
                var images = _mapper.Map<List<CompanyImage>>(request.ImageUrls);
                _unitOfWork.Repository<CompanyImage>().AddRange(images);

                await _unitOfWork.Complete();
            }


            return _mapper.Map<CompanyVm>(companyToUpdate);
        }
    }
}
