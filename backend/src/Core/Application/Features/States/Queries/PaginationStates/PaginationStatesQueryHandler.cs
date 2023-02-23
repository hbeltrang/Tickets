using AutoMapper;
using MediatR;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Application.Features.Shared.Queries;
using Tickets.Application.Features.States.Vms;
using Tickets.Application.Persistence;
using Tickets.Application.Specifications.States;
using Tickets.Domain;

namespace Tickets.Application.Features.States.Queries.PaginationStates
{
    public class PaginationStatesQueryHandler : IRequestHandler<PaginationStatesQuery, PaginationVm<StateVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationStatesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationVm<StateVm>> Handle(PaginationStatesQuery request, CancellationToken cancellationToken)
        {
            var stateSpecificationParams = new StateSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
                CountryId = request.CountryId,
            };

            var spec = new StateSpecification(stateSpecificationParams);
            var states = await _unitOfWork.Repository<State>().GetAllWithSpec(spec);

            var specCount = new StateForCountingSpecification(stateSpecificationParams);
            var totalStates = await _unitOfWork.Repository<State>().CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalStates) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<StateVm>>(states);
            var productsByPage = states.Count();

            var pagination = new PaginationVm<StateVm>
            {
                Count = totalStates,
                Data = data,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = productsByPage
            };

            return pagination;
        }
    }
}
