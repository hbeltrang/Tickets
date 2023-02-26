using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Categories.Commands.UpdateCategory;
using Tickets.Application.Features.Categories.Vms;
using Tickets.Application.Features.Taxes.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Taxes.Commands.UpdateTax
{
    public class UpdateTaxCommandHandler : IRequestHandler<UpdateTaxCommand, TaxVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTaxCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TaxVm> Handle(UpdateTaxCommand request, CancellationToken cancellationToken)
        {
            var taxToUpdate = await _unitOfWork.Repository<Tax>().GetByIdAsync(request.Id);
            if (taxToUpdate is null)
            {
                throw new NotFoundException(nameof(Tax), request.Id);
            }

            _mapper.Map(request, taxToUpdate, typeof(UpdateTaxCommand), typeof(Tax));
            await _unitOfWork.Repository<Tax>().UpdateAsync(taxToUpdate);
            await _unitOfWork.Complete();

            if (taxToUpdate.IsDefault == true)
            {
                //remove isdefault=true other records
                var taxes = await _unitOfWork.Repository<Tax>().GetAsync(
                    x => x.Status == Status.Active && x.Id != request.Id,
                    x => x.OrderBy(y => y.Name),
                    string.Empty,
                    false
                );

                if (taxes != null)
                {
                    //update others records in isdefault=false
                    foreach (var item in taxes)
                    {
                        item.IsDefault = false;
                        _unitOfWork.Repository<Tax>().UpdateEntity(item);
                    }
                    await _unitOfWork.Complete();
                }
            }

            return _mapper.Map<TaxVm>(taxToUpdate);
        }
    }
}
