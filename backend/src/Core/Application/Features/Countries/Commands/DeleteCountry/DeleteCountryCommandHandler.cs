using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Application.Features.Categories.Commands.DeleteCategory;
using Tickets.Application.Features.Categories.Vms;
using Tickets.Application.Features.Countries.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain.Common;
using Tickets.Domain;
using Tickets.Application.Exceptions;

namespace Tickets.Application.Features.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, CountryVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CountryVm> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var countryToUpdate = await _unitOfWork.Repository<Country>().GetByIdAsync(request.CountryId);
            if (countryToUpdate is null)
            {
                throw new NotFoundException(nameof(Country), request.CountryId);
            }

            countryToUpdate.Status = countryToUpdate.Status == Status.Inactive
            ? Status.Active : Status.Inactive;


            await _unitOfWork.Repository<Country>().UpdateAsync(countryToUpdate);

            return _mapper.Map<CountryVm>(countryToUpdate);
        }
    }
}
