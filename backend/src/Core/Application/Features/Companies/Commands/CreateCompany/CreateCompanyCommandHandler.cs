using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Application.Features.Companies.Vms;
using Tickets.Application.Features.Socials.Commands.CreateSocial;
using Tickets.Application.Features.Socials.Vms;
using Tickets.Application.Messages;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyVm>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCompanyCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CompanyVm> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Company>(request);
            await _unitOfWork.Repository<Company>().AddAsync(entity);

            if ((request.ImageUrls is not null) && request.ImageUrls.Count > 0)
            {
                request.ImageUrls.Select(c => { c.CompanyId = entity.Id; return c; }).ToList();

                var images = _mapper.Map<List<CompanyImage>>(request.ImageUrls);
                _unitOfWork.Repository<CompanyImage>().AddRange(images);
            }

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("Company: " + MessageLabel.ErrorToSave);
            }

            return _mapper.Map<CompanyVm>(entity);
        }
    }
}
