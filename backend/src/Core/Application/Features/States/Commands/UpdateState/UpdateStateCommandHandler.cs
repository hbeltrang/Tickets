using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.States.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.States.Commands.UpdateState
{
    public class UpdateStateCommandHandler : IRequestHandler<UpdateStateCommand, StateVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StateVm> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
        {
            var stateToUpdate = await _unitOfWork.Repository<State>().GetByIdAsync(request.Id);
            if (stateToUpdate is null)
            {
                throw new NotFoundException(nameof(State), request.Id);
            }

            var slug = request.Name!.ToLower();
            request.slug = slug.Replace(" ", "-");

            _mapper.Map(request, stateToUpdate, typeof(UpdateStateCommand), typeof(State));
            await _unitOfWork.Repository<State>().UpdateAsync(stateToUpdate);
            await _unitOfWork.Complete();
            

            return _mapper.Map<StateVm>(stateToUpdate);
        }
    }
}
