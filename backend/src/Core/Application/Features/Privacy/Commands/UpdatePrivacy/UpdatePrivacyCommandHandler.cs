using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Privacy.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Privacy.Commands.UpdatePrivacy
{
    public class UpdatePrivacyCommandHandler : IRequestHandler<UpdatePrivacyCommand, PrivacyVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePrivacyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PrivacyVm> Handle(UpdatePrivacyCommand request, CancellationToken cancellationToken)
        {
            var privacyToUpdate = await _unitOfWork.Repository<PrivacyPolicy>().GetByIdAsync(request.Id);
            if (privacyToUpdate is null)
            {
                throw new NotFoundException(nameof(Privacy), request.Id);
            }

            _mapper.Map(request, privacyToUpdate, typeof(UpdatePrivacyCommand), typeof(PrivacyPolicy));
            await _unitOfWork.Repository<PrivacyPolicy>().UpdateAsync(privacyToUpdate);
            await _unitOfWork.Complete();            

            return _mapper.Map<PrivacyVm>(privacyToUpdate);
        }
    }
}
