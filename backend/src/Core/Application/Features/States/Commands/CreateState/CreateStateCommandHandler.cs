using AutoMapper;
using MediatR;
using Tickets.Application.Features.Countries.Vms;
using Tickets.Application.Features.States.Vms;
using Tickets.Application.Messages;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.States.Commands.CreateState
{
    public class CreateStateCommandHandler : IRequestHandler<CreateStateCommand, StateVm>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CreateStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StateVm> Handle(CreateStateCommand request, CancellationToken cancellationToken)
        {
            var slug = request.Name!.ToLower();
            request.Slug = slug.Replace(" ", "-");

            //var entity = new State
            //{
            //    Name = request.Name,
            //    Code = request.Code,
            //    Slug = request.Slug,
            //    CountryId = (int)request.CountryId!,
            //};

            //await _unitOfWork.Repository<State>().AddAsync(entity);
            //var result = await _unitOfWork.Complete();

            //if (result <= 0)
            //{
            //    throw new Exception("State: " + MessageLabel.ErrorToSave);
            //}

            //return _mapper.Map<StateVm>(entity);


            var stateEntity = _mapper.Map<State>(request);
            await _unitOfWork.Repository<State>().AddAsync(stateEntity);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("State: " + MessageLabel.ErrorToSave);
            }

            return _mapper.Map<StateVm>(stateEntity);
        }



    }
}
