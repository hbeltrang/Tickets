using AutoMapper;
using MediatR;
using Tickets.Application.Features.Cities.Vms;
using Tickets.Application.Messages;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CityVm>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CreateCityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CityVm> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<City>(request);
            await _unitOfWork.Repository<City>().AddAsync(entity);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("City: " + MessageLabel.ErrorToSave);
            }

            return _mapper.Map<CityVm>(entity);
        }
    }
}
