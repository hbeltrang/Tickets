using AutoMapper;
using MediatR;
using Tickets.Application.Features.Privacy.Vms;
using Tickets.Application.Messages;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Privacy.Commands.CreatePrivacy
{
    public class CreatePrivacyCommandHandler : IRequestHandler<CreatePrivacyCommand, PrivacyVm>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePrivacyCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PrivacyVm> Handle(CreatePrivacyCommand request, CancellationToken cancellationToken)
        {            
            var entity = new PrivacyPolicy
            {
                Name = request.Name,
                Content = request.Content!,
            };

            _unitOfWork.Repository<PrivacyPolicy>().AddEntity(entity);
            var resultado = await _unitOfWork.Complete();

            if (resultado <= 0)
            {
                throw new Exception("PrivacyPolicy: " + MessageLabel.ErrorToSave);
            }

            return _mapper.Map<PrivacyVm>(entity);
        }
    }
}
