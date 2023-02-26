using AutoMapper;
using MediatR;
using Tickets.Application.Features.Terms.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Terms.Queries.GetTermList
{
    public class GetTermListQueryHandler : IRequestHandler<GetTermListQuery, IReadOnlyList<TermVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTermListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TermVm>> Handle(GetTermListQuery request, CancellationToken cancellationToken)
        {
            var terms = await _unitOfWork.Repository<Term>().GetAsync(
                x => x.Status == Status.Active,
                x => x.OrderBy(y => y.Name),
                string.Empty,
                false
            );

            return _mapper.Map<IReadOnlyList<TermVm>>(terms);
        }
    }
}
