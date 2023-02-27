using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Promoters.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Promoters.Commands.DeletePromoter
{
    public class DeletePromoterCommandHandler : IRequestHandler<DeletePromoterCommand, PromoterVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePromoterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PromoterVm> Handle(DeletePromoterCommand request, CancellationToken cancellationToken)
        {
            var promoterToUpdate = await _unitOfWork.Repository<Promoter>().GetByIdAsync(request.PromoterId);
            if (promoterToUpdate is null)
            {
                throw new NotFoundException(nameof(Promoter), request.PromoterId);
            }

            promoterToUpdate.Status = promoterToUpdate.Status == Status.Inactive
                            ? Status.Active : Status.Inactive;


            await _unitOfWork.Repository<Promoter>().UpdateAsync(promoterToUpdate);

            return _mapper.Map<PromoterVm>(promoterToUpdate);
        }
    }
}
