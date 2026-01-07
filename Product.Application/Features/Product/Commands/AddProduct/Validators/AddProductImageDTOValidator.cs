using Product.Application.Features.Product.Commands.AddProduct.DTOs;

namespace Product.Application.Features.Product.Commands.AddProduct.Validators
{
    internal class AddProductImageDTOValidator : AbstractValidator<AddProductImageDTO>
    {
        public AddProductImageDTOValidator(IGenericRepository<Domain.Entities.Color> colorRepo)
        {
            RuleFor(img => img.ImageFile)
                   .NotNull().WithMessage(Messages.EmptyField);

            RuleFor(img => img.ColorCode)
                .NotNull().WithMessage(Messages.EmptyField)
                .Must(code => code.StartsWith('#')).WithMessage(Messages.IncorrectData);
        }
    }
}
