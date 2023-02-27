using AutoMapper;
using MediatR;
using Tickets.Application.Features.Taxes.Vms;
using Tickets.Application.Messages;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Taxes.Commands.CreateTax
{
    public class CreateTaxCommandHandler : IRequestHandler<CreateTaxCommand, TaxVm>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaxCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TaxVm> Handle(CreateTaxCommand request, CancellationToken cancellationToken)
        {
            //No hay registros con un default se asigna el registro que se esta creando
            var taxes = await _unitOfWork.Repository<Tax>().GetAsync(
                    x => x.Status == Status.Active && x.IsDefault == true,
                    x => x.OrderBy(y => y.Name),
                    string.Empty,
                    false
                );

            if (taxes == null)
            {
                request.IsDefault = true;
            }
            else
            {
                if (request.IsDefault == true)
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

            var entity = new Tax
            {
                Name = request.Name,
                Percent = (decimal)request.Percent!,
                IsDefault = (bool)request.IsDefault!,
            };

            _unitOfWork.Repository<Tax>().AddEntity(entity);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("Tax: " + MessageLabel.ErrorToSave);
            }

            return _mapper.Map<TaxVm>(entity);
        }
    }
}
