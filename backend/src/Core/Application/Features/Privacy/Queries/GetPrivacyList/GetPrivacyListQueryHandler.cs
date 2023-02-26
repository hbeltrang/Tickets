using AutoMapper;
using MediatR;
using Tickets.Application.Features.Privacy.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Privacy.Queries.GetPrivacyList
{
    public class GetPrivacyListQueryHandler : IRequestHandler<GetPrivacyListQuery, IReadOnlyList<PrivacyVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPrivacyListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<PrivacyVm>> Handle(GetPrivacyListQuery request, CancellationToken cancellationToken)
        {
            var privacys = await _unitOfWork.Repository<PrivacyPolicy>().GetAsync(
                x => x.Status == Status.Active,
                x => x.OrderBy(y => y.Name),
                string.Empty,
                false
            );

            return _mapper.Map<IReadOnlyList<PrivacyVm>>(privacys);
        }
    }
}
