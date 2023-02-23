using AutoMapper;
using MediatR;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Application.Features.States.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.States.Commands.CreateState
{
    public class CreateStateCommandHandler : IRequestHandler<CreateStateCommand, StateVm>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CreateStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StateVm> Handle(CreateStateCommand request, CancellationToken cancellationToken)
        {
            var stateEntity = _mapper.Map<State>(request);
            await _unitOfWork.Repository<State>().AddAsync(stateEntity);
            await _unitOfWork.Complete();

            return _mapper.Map<StateVm>(stateEntity);
        }



    }
}
