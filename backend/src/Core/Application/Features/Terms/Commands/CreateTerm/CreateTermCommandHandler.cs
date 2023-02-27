using AutoMapper;
using MediatR;
using Tickets.Application.Features.Terms.Vms;
using Tickets.Application.Messages;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Terms.Commands.CreateTerm
{
    public class CreateTermCommandHandler : IRequestHandler<CreateTermCommand, TermVm>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTermCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TermVm> Handle(CreateTermCommand request, CancellationToken cancellationToken)
        {            
            var entity = new Term
            {
                Name = request.Name,
                Content = request.Content!,
            };

            _unitOfWork.Repository<Term>().AddEntity(entity);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("Term: " + MessageLabel.ErrorToSave);
            }

            return _mapper.Map<TermVm>(entity);
        }
    }
}
