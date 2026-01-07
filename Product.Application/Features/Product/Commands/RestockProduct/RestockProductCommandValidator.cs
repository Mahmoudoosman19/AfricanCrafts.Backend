using Product.Domain.Entities;

namespace Product.Application.Features.Product.Commands.RestockProduct
{
    public class RestockProductCommandValidator : AbstractValidator<RestockProductCommand>
    {

        public RestockProductCommandValidator(
            IGenericRepository<ProductExtension> productextensionRepo)
        {
            RuleFor(x => x.ProductExtensionId)
            .NotEmpty()
            .WithMessage(Messages.EmptyField)
            .EntityExist(productextensionRepo);

            RuleFor(x => x.Amount)
            .NotEmpty()
            .WithMessage(Messages.EmptyField)
            .GreaterThan(0).WithMessage(Messages.IncorrectData);
            RuleFor(x => x.Increase)
           .NotNull()
           .WithMessage(Messages.EmptyField);
           

        }
    }
}
