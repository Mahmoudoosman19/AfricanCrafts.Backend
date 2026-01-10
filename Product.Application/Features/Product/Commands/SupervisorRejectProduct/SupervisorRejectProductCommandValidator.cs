using Product.Domain.Abstraction;

namespace Product.Application.Features.Product.Commands.SupervisorRejectProduct;

public class SupervisorRejectProductCommandValidator
    : AbstractValidator<SupervisorRejectProductCommand>
{
    public SupervisorRejectProductCommandValidator(
        IProductRepository<Domain.Entities.Product> productRepo)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .EntityExist(productRepo).WithMessage(Messages.NotFound);
    }
}