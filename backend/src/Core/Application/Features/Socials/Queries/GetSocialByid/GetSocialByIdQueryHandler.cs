using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.Socials.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Socials.Queries.GetSocialById
{
    public class GetSocialByidQueryHandler : IRequestHandler<GetSocialByIdQuery, SocialVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSocialByidQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SocialVm> Handle(GetSocialByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Social, object>>>();
            includes.Add(p => p.SocialImages!);

            var socials = await _unitOfWork.Repository<Social>().GetEntityAsync(
                x => x.Id == request.SocialId && x.Status == Status.Active,
                includes,
                true
            );

            return _mapper.Map<SocialVm>(socials);
        }
    }
}
