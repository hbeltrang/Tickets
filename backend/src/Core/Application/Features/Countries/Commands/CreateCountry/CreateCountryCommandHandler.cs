using AutoMapper;
using MediatR;
using Tickets.Application.Features.Countries.Vms;
using Tickets.Application.Messages;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CountryVm>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCountryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CountryVm> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Country
            {
                Name = request.Name,
                Code = request.Code,
            };

            _unitOfWork.Repository<Country>().AddEntity(entity);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("Country: " + MessageLabel.ErrorToSave);
            }

            return _mapper.Map<CountryVm>(entity);
        }
    }
}
