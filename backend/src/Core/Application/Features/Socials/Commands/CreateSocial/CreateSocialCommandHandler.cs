using AutoMapper;
using MediatR;
using Tickets.Application.Features.Socials.Vms;
using Tickets.Application.Messages;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Socials.Commands.CreateSocial
{
    public class CreateSocialCommandHandler : IRequestHandler<CreateSocialCommand, SocialVm>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSocialCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<SocialVm> Handle(CreateSocialCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Social>(request);
            await _unitOfWork.Repository<Social>().AddAsync(entity);

            if ((request.ImageUrls is not null) && request.ImageUrls.Count > 0)
            {
                request.ImageUrls.Select(c => { c.SocialId = entity.Id; return c; }).ToList();

                var images = _mapper.Map<List<SocialImage>>(request.ImageUrls);
                _unitOfWork.Repository<SocialImage>().AddRange(images);
            }

            var resultado = await _unitOfWork.Complete();

            if (resultado <= 0)
            {
                throw new Exception("Social: " + MessageLabel.ErrorToSave);
            }

            return _mapper.Map<SocialVm>(entity);
        }
    }
}
