using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Sizes.Commands.ToggleSizeStop
{
    internal class ToggleSizeActivationCommandValidator : AbstractValidator<ToggleSizeActivationCommand>
    {
        public ToggleSizeActivationCommandValidator(IProductRepository<Size> sizeRepo)
        {
            RuleFor(x => x.SizeId)
            .NotNull()
            .EntityExist(sizeRepo).WithMessage(Messages.NotFound);
        }
    }
}
