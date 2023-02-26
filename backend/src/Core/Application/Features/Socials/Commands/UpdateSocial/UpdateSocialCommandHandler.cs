using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Socials.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Socials.Commands.UpdateSocial
{
    public class UpdateSocialCommandHandler : IRequestHandler<UpdateSocialCommand, SocialVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSocialCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SocialVm> Handle(UpdateSocialCommand request, CancellationToken cancellationToken)
        {
            var socialToUpdate = await _unitOfWork.Repository<Social>().GetByIdAsync(request.Id);
            if (socialToUpdate is null)
            {
                throw new NotFoundException(nameof(Social), request.Id);
            }

            _mapper.Map(request, socialToUpdate, typeof(UpdateSocialCommand), typeof(Social));
            await _unitOfWork.Repository<Social>().UpdateAsync(socialToUpdate);

            if ((request.ImageUrls is not null) && request.ImageUrls.Count > 0)
            {
                var imagesToRemove = await _unitOfWork.Repository<SocialImage>().GetAsync(
                    x => x.SocialId == request.Id
                );

                _unitOfWork.Repository<SocialImage>().DeleteRange(imagesToRemove);

                request.ImageUrls.Select(c => { c.SocialId = request.Id; return c; }).ToList();
                var images = _mapper.Map<List<SocialImage>>(request.ImageUrls);
                _unitOfWork.Repository<SocialImage>().AddRange(images);

                await _unitOfWork.Complete();
            }


            return _mapper.Map<SocialVm>(socialToUpdate);
        }
    }
}
