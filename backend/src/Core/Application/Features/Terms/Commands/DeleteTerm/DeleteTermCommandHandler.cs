using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Terms.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Terms.Commands.DeleteTerm
{
    public class DeleteTermCommandHandler : IRequestHandler<DeleteTermCommand, TermVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteTermCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TermVm> Handle(DeleteTermCommand request, CancellationToken cancellationToken)
        {
            var termToUpdate = await _unitOfWork.Repository<Term>().GetByIdAsync(request.TermId);
            if (termToUpdate is null)
            {
                throw new NotFoundException(nameof(Term), request.TermId);
            }

            termToUpdate.Status = termToUpdate.Status == Status.Inactive
            ? Status.Active : Status.Inactive;


            await _unitOfWork.Repository<Term>().UpdateAsync(termToUpdate);

            return _mapper.Map<TermVm>(termToUpdate);
        }
    }
}
