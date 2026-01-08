using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Categories.Commands.ToggleCategoryActivation
{
    public class ToggleCategoryActivationCommandHandler : ICommandHandler<ToggleCategoryActivationCommand>
    {
        private readonly IProductRepository<Category> _categoryRepo;

        public ToggleCategoryActivationCommandHandler(IProductRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public async Task<ResponseModel> Handle(ToggleCategoryActivationCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepo.GetByIdAsync(request.Id);

            category!.SetActivation(!category.IsActive);

            _categoryRepo.Update(category);
            await _categoryRepo.SaveChangesAsync();

            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
