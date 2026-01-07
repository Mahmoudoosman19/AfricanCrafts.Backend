using Product.Domain.Entities;

namespace Product.Application.Features.Sizes.Commands.ToggleSizeStop
{
    internal class ToggleSizeActivationCommandValidator : AbstractValidator<ToggleSizeActivationCommand>
    {
        public ToggleSizeActivationCommandValidator(IGenericRepository<Size> sizeRepo)
        {
            RuleFor(x => x.SizeId)
            .NotNull()
            .EntityExist(sizeRepo).WithMessage(Messages.NotFound);
        }
    }
}
