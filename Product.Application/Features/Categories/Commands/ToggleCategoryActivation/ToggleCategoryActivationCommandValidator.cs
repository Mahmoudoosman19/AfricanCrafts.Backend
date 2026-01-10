using Product.Application.Features.Categories.Commands.ToggleCategoryActivation;
using Product.Domain.Abstraction;

internal class ToggleCategoryActivationCommandValidator : AbstractValidator<ToggleCategoryActivationCommand>
{
    public ToggleCategoryActivationCommandValidator(IProductRepository<Product.Domain.Entities.Category> categoryRepo)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .EntityExist(categoryRepo).WithMessage(Messages.NotFound);
    }
}