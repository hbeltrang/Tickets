using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Cities.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Cities.Commands.DeleteCity
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, CityVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CityVm> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var cityToUpdate = await _unitOfWork.Repository<City>().GetByIdAsync(request.CityId);
            if (cityToUpdate is null)
            {
                throw new NotFoundException(nameof(City), request.CityId);
            }

            cityToUpdate.Status = cityToUpdate.Status == Status.Inactive
                            ? Status.Active : Status.Inactive;


            await _unitOfWork.Repository<City>().UpdateAsync(cityToUpdate);

            return _mapper.Map<CityVm>(cityToUpdate);
        }
    }
}
