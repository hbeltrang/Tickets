using AutoMapper;
using MediatR;
using Tickets.Application.Features.Promoters.Vms;
using Tickets.Application.Features.Terms.Vms;
using Tickets.Application.Messages;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Promoters.Commands.CreatePromoter
{
    public class CreatePromoterCommandHandler : IRequestHandler<CreatePromoterCommand, PromoterVm>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePromoterCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PromoterVm> Handle(CreatePromoterCommand request, CancellationToken cancellationToken)
        {
            var entity = new Promoter
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                CellPhone = request.CellPhone,
                Address = request.Address,
                FeeAmount = request.FeeAmount,
                FeePercent = request.FeePercent,
                NotifyEmail = request.NotifyEmail,
                NotifyEmailBcc = request.NotifyEmailBcc,
                NotifyCellPhone = request.NotifyCellPhone,
            };

            _unitOfWork.Repository<Promoter>().AddEntity(entity);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("Promoter: " + MessageLabel.ErrorToSave);
            }

            return _mapper.Map<PromoterVm>(entity);
        }
    }
}
