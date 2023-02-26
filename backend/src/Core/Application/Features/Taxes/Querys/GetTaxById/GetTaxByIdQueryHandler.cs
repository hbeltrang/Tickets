using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Tickets.Application.Features.Taxes.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Taxes.Querys.GetTaxById
{
    public class GetTaxByIdQueryHandler : IRequestHandler<GetTaxByIdQuery, TaxVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTaxByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TaxVm> Handle(GetTaxByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Tax, object>>>();

            var taxes = await _unitOfWork.Repository<Tax>().GetEntityAsync(
                x => x.Id == request.TaxId && x.Status == Status.Active,
                includes,
                true
            );

            return _mapper.Map<TaxVm>(taxes);
        }
    }
}
