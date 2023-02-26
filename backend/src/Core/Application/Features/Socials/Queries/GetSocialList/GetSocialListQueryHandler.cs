using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.Socials.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Socials.Queries.GetSocialList
{
    public class GetSocialListQueryHandler : IRequestHandler<GetSocialListQuery, IReadOnlyList<SocialVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSocialListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<SocialVm>> Handle(GetSocialListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Social, object>>>();
            includes.Add(p => p.SocialImages!);

            var socials = await _unitOfWork.Repository<Social>().GetAsync(
                x => x.Status == Status.Active,
                x => x.OrderBy(y => y.Name),
                includes,
                false
            );

            return _mapper.Map<IReadOnlyList<SocialVm>>(socials);
        }
    }
}
