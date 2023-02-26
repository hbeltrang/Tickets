using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Socials.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Socials.Commands.DeleteSocial
{
    public class DeleteSocialCommandHandler : IRequestHandler<DeleteSocialCommand, SocialVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSocialCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SocialVm> Handle(DeleteSocialCommand request, CancellationToken cancellationToken)
        {
            var socialToUpdate = await _unitOfWork.Repository<Social>().GetByIdAsync(request.SocialId);
            if (socialToUpdate is null)
            {
                throw new NotFoundException(nameof(Social), request.SocialId);
            }

            socialToUpdate.Status = socialToUpdate.Status == Status.Inactive
                            ? Status.Active : Status.Inactive;


            await _unitOfWork.Repository<Social>().UpdateAsync(socialToUpdate);

            return _mapper.Map<SocialVm>(socialToUpdate);
        }
    }
}
