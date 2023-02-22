using AutoMapper;
using MediatR;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tickets.Application.Features.Categories.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryVm> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Category, object>>>();

            var categories = await _unitOfWork.Repository<Category>().GetEntityAsync(
                x => x.Id == request.CategoryId && x.Status == Status.Active,
                includes,
                true
            );

            return _mapper.Map<CategoryVm>(categories);
        }
    }
}
