using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.States.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.States.Commands.DeleteState
{
    public class DeleteStateCommandHandler : IRequestHandler<DeleteStateCommand, StateVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StateVm> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
        {
            var stateToUpdate = await _unitOfWork.Repository<State>().GetByIdAsync(request.StateId);
            if (stateToUpdate is null)
            {
                throw new NotFoundException(nameof(State), request.StateId);
            }

            stateToUpdate.Status = stateToUpdate.Status == Status.Inactive
                            ? Status.Active : Status.Inactive;


            await _unitOfWork.Repository<State>().UpdateAsync(stateToUpdate);

            return _mapper.Map<StateVm>(stateToUpdate);
        }
    }
}
