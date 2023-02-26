using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Application.Features.Categories.Commands.DeleteCategory;
using Tickets.Application.Features.Categories.Vms;
using Tickets.Application.Features.Taxes.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain.Common;
using Tickets.Domain;
using Tickets.Application.Exceptions;

namespace Tickets.Application.Features.Taxes.Commands.DeleteTax
{
    public class DeleteTaxCommandHandler : IRequestHandler<DeleteTaxCommand, TaxVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteTaxCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TaxVm> Handle(DeleteTaxCommand request, CancellationToken cancellationToken)
        {
            var taxToUpdate = await _unitOfWork.Repository<Tax>().GetByIdAsync(request.TaxId);
            if (taxToUpdate is null)
            {
                throw new NotFoundException(nameof(Tax), request.TaxId);
            }

            taxToUpdate.Status = taxToUpdate.Status == Status.Inactive
            ? Status.Active : Status.Inactive;


            await _unitOfWork.Repository<Tax>().UpdateAsync(taxToUpdate);

            return _mapper.Map<TaxVm>(taxToUpdate);
        }
    }
}
