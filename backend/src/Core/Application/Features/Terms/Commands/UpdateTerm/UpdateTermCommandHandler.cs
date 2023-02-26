using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Terms.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Terms.Commands.UpdateTerm
{
    public class UpdateTermCommandHandler : IRequestHandler<UpdateTermCommand, TermVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTermCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TermVm> Handle(UpdateTermCommand request, CancellationToken cancellationToken)
        {
            var termToUpdate = await _unitOfWork.Repository<Term>().GetByIdAsync(request.Id);
            if (termToUpdate is null)
            {
                throw new NotFoundException(nameof(Term), request.Id);
            }

            _mapper.Map(request, termToUpdate, typeof(UpdateTermCommand), typeof(Term));
            await _unitOfWork.Repository<Term>().UpdateAsync(termToUpdate);
            await _unitOfWork.Complete();            

            return _mapper.Map<TermVm>(termToUpdate);
        }
    }
}
