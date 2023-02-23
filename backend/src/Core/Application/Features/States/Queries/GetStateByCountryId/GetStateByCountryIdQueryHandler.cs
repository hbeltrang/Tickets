using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.States.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.States.Queries.GetStateByCountryId
{
    public class GetStateByCountryIdQueryHandler : IRequestHandler<GetStateByCountryIdQuery, IReadOnlyList<StateVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetStateByCountryIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<StateVm>> Handle(GetStateByCountryIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<State, object>>>();
            includes.Add(p => p.Country!);

            var states = await _unitOfWork.Repository<State>().GetAsync(
               x => x.Status == Status.Active && x.CountryId == request.CountryId,
               x => x.OrderBy(y => y.Name),
               includes,
               false
           );

            var statesVm = _mapper.Map<IReadOnlyList<StateVm>>(states);

            return statesVm;

        }
    }
}
