using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.Terms.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Terms.Queries.GetTermById
{
    public class GetTermByIdQueryHandler : IRequestHandler<GetTermByIdQuery, TermVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTermByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TermVm> Handle(GetTermByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Term, object>>>();

            var terms = await _unitOfWork.Repository<Term>().GetEntityAsync(
                x => x.Id == request.TermId && x.Status == Status.Active,
                includes,
                true
            );

            return _mapper.Map<TermVm>(terms);
        }
    }
}
