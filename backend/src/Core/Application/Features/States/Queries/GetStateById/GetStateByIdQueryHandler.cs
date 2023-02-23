using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.States.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.States.Queries.GetStateById
{
    public class GetStateByIdQueryHandler : IRequestHandler<GetStateByIdQuery, StateVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetStateByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StateVm> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<State, object>>>();
            includes.Add(p => p.Country!);

            var states = await _unitOfWork.Repository<State>().GetEntityAsync(
                x => x.Id == request.StateId && x.Status == Status.Active,
                includes,
                false
            );

            return _mapper.Map<StateVm>(states);
        }
    }
}
