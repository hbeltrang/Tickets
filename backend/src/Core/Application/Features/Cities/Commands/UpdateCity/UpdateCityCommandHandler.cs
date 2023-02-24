using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Cities.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Cities.Commands.UpdateCity
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, CityVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CityVm> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var cityToUpdate = await _unitOfWork.Repository<City>().GetByIdAsync(request.Id);
            if (cityToUpdate is null)
            {
                throw new NotFoundException(nameof(City), request.Id);
            }

            _mapper.Map(request, cityToUpdate, typeof(UpdateCityCommand), typeof(City));
            await _unitOfWork.Repository<City>().UpdateAsync(cityToUpdate);
            await _unitOfWork.Complete();


            return _mapper.Map<CityVm>(cityToUpdate);
        }
    }
}
