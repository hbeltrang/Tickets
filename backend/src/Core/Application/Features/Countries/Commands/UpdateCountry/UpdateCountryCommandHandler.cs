using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Countries.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, CountryVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CountryVm> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var countryToUpdate = await _unitOfWork.Repository<Country>().GetByIdAsync(request.Id);
            if (countryToUpdate is null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }

            _mapper.Map(request, countryToUpdate, typeof(UpdateCountryCommand), typeof(Country));
            await _unitOfWork.Repository<Country>().UpdateAsync(countryToUpdate);
            await _unitOfWork.Complete();

            return _mapper.Map<CountryVm>(countryToUpdate);
        }
    }
}
