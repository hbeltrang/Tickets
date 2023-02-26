using AutoMapper;
using MediatR;
using Tickets.Application.Features.Taxes.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Taxes.Querys.GetTaxList
{
    public class GetTaxListQueryHandler : IRequestHandler<GetTaxListQuery, IReadOnlyList<TaxVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTaxListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TaxVm>> Handle(GetTaxListQuery request, CancellationToken cancellationToken)
        {
            var taxes = await _unitOfWork.Repository<Tax>().GetAsync(
                x => x.Status == Status.Active,
                x => x.OrderBy(y => y.Name),
                string.Empty,
                false
            );

            return _mapper.Map<IReadOnlyList<TaxVm>>(taxes);
        }
    }
}
