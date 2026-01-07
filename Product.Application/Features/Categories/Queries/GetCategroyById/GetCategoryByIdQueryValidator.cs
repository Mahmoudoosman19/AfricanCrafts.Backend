using Product.Domain.Entities;

namespace Product.Application.Features.Categories.Queries.GetOneCategroy
{
    internal class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdWithSizeGroupAndParentCategoryQuery>
    {
        public GetCategoryByIdQueryValidator(
       IGenericRepository<Category> CategoryRepo)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EntityExist(CategoryRepo).WithMessage(Messages.NotFound);
        }
    }
}
