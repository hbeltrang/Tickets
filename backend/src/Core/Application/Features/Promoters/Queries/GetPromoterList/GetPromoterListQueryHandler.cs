using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.Promoters.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Promoters.Queries.GetPromoterList
{
    public class GetPromoterListQueryHandler : IRequestHandler<GetPromoterListQuery, IReadOnlyList<PromoterVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPromoterListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<PromoterVm>> Handle(GetPromoterListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Promoter, object>>>();

            var promoters = await _unitOfWork.Repository<Promoter>().GetAsync(
                x => x.Status == Status.Active,
                x => x.OrderBy(y => y.Name),
                includes,
                false
            );

            return _mapper.Map<IReadOnlyList<PromoterVm>>(promoters);
        }
    }
}
