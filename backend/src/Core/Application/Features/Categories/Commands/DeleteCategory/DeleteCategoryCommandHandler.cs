using AutoMapper;
using MediatR;
using Tickets.Application.Exceptions;
using Tickets.Application.Features.Categories.Vms;
using Tickets.Application.Persistence;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, CategoryVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryVm> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToUpdate = await _unitOfWork.Repository<Category>().GetByIdAsync(request.CategoryId);
            if (categoryToUpdate is null)
            {
                throw new NotFoundException(nameof(Category), request.CategoryId);
            }

            categoryToUpdate.Status = categoryToUpdate.Status == Status.Inactive
            ? Status.Active : Status.Inactive;


            await _unitOfWork.Repository<Category>().UpdateAsync(categoryToUpdate);

            return _mapper.Map<CategoryVm>(categoryToUpdate);
        }
    }
}
