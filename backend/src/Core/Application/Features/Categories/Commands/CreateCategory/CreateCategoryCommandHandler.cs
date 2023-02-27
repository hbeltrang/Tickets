using AutoMapper;
using MediatR;
using Tickets.Application.Features.Categories.Vms;
using Tickets.Application.Messages;
using Tickets.Application.Persistence;
using Tickets.Domain;

namespace Tickets.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryVm>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryVm> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Category
            {
                Name = request.Name,
            };

            _unitOfWork.Repository<Category>().AddEntity(entity);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("Category: " + MessageLabel.ErrorToSave);
            }

            return _mapper.Map<CategoryVm>(entity);
        }
    }
}
