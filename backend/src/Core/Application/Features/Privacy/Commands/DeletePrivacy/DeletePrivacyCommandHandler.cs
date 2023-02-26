using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Privacy.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Privacy.Commands.DeletePrivacy
{
    public class DeletePrivacyCommandHandler : IRequestHandler<DeletePrivacyCommand, PrivacyVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePrivacyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PrivacyVm> Handle(DeletePrivacyCommand request, CancellationToken cancellationToken)
        {
            var privacyToUpdate = await _unitOfWork.Repository<PrivacyPolicy>().GetByIdAsync(request.PrivacyId);
            if (privacyToUpdate is null)
            {
                throw new NotFoundException(nameof(PrivacyPolicy), request.PrivacyId);
            }

            privacyToUpdate.Status = privacyToUpdate.Status == Status.Inactive
            ? Status.Active : Status.Inactive;


            await _unitOfWork.Repository<PrivacyPolicy>().UpdateAsync(privacyToUpdate);

            return _mapper.Map<PrivacyVm>(privacyToUpdate);
        }
    }
}
