using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.Promoters.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Promoters.Queries.GetPromoterById
{
    public class GetPromoterByIdQueryHandler : IRequestHandler<GetPromoterByIdQuery, PromoterVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPromoterByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PromoterVm> Handle(GetPromoterByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Promoter, object>>>();

            var promoters = await _unitOfWork.Repository<Promoter>().GetEntityAsync(
                x => x.Id == request.PromoterId && x.Status == Status.Active,
                includes,
                true
            );

            return _mapper.Map<PromoterVm>(promoters);
        }
    }
}
