using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.Privacy.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Privacy.Queries.GetPrivacyById
{
    public class GetPrivacyByIdQueryHandler : IRequestHandler<GetPrivacyByIdQuery, PrivacyVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPrivacyByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PrivacyVm> Handle(GetPrivacyByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<PrivacyPolicy, object>>>();

            var privacys = await _unitOfWork.Repository<PrivacyPolicy>().GetEntityAsync(
                x => x.Id == request.PrivacyId && x.Status == Status.Active,
                includes,
                true
            );

            return _mapper.Map<PrivacyVm>(privacys);
        }
    }
}
