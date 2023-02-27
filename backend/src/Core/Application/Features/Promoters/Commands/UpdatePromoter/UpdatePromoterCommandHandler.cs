using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Promoters.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Promoters.Commands.UpdatePromoter
{
    public class UpdatePromoterCommandHandler : IRequestHandler<UpdatePromoterCommand, PromoterVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePromoterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PromoterVm> Handle(UpdatePromoterCommand request, CancellationToken cancellationToken)
        {
            var promoterToUpdate = await _unitOfWork.Repository<Promoter>().GetByIdAsync(request.Id);
            if (promoterToUpdate is null)
            {
                throw new NotFoundException(nameof(Promoter), request.Id);
            }

            _mapper.Map(request, promoterToUpdate, typeof(UpdatePromoterCommand), typeof(Promoter));
            await _unitOfWork.Repository<Promoter>().UpdateAsync(promoterToUpdate);
            await _unitOfWork.Complete();

            return _mapper.Map<PromoterVm>(promoterToUpdate);
        }
    }
}
