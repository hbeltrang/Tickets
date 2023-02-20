using AutoMapper;
using MediatR;
using Tickets.Application.Features.Categories.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, IReadOnlyList<CategoryVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryVm>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Repository<Category>().GetAsync(
                null,
                x => x.OrderBy(y => y.Name),
                string.Empty,
                false
            );

            return _mapper.Map<IReadOnlyList<CategoryVm>>(categories);

        }
    }
}
