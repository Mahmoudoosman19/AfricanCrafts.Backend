using Product.Application.Features.Categories.Commands.ToggleCategoryActivation;

internal class ToggleCategoryActivationCommandValidator : AbstractValidator<ToggleCategoryActivationCommand>
{
    public ToggleCategoryActivationCommandValidator(IGenericRepository<Product.Domain.Entities.Category> categoryRepo)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .EntityExist(categoryRepo).WithMessage(Messages.NotFound);
    }
}