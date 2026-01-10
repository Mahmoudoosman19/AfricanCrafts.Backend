using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Categories.Queries.GetSubCategories;

internal class GetSubCategoriesValidator : AbstractValidator<GetSubCategoriesQuery>
{

    public GetSubCategoriesValidator(
        IProductRepository<Category> categoryRepo)
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage(Messages.EmptyField)
            .EntityExist(categoryRepo).WithMessage(Messages.NotFound);
    }
}
