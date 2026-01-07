using Product.Application.Features.Product.Commands.UpdateProduct.DTOs;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.Commands.UpdateProduct.Validators
{
    internal class UpdateProductImageDTOValidator : AbstractValidator<UpdateProductImageDTO>
    {
        public UpdateProductImageDTOValidator(IGenericRepository<ProductImage> productImageRepo, IGenericRepository<Domain.Entities.Color> colorRepo)
        {
            When(img => img.Id != Guid.Empty, () =>
            {
                RuleFor(img => img.Id)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EntityExist(productImageRepo).WithMessage(Messages.NotFound);
            }
            ).Otherwise(() =>
            {
                RuleFor(img => img.ImageFile)
                .NotNull().WithMessage(Messages.EmptyField);
            });

            RuleFor(img => img.ColorCode)
                .NotNull().WithMessage(Messages.EmptyField)
                .Must(code => code.StartsWith('#')).WithMessage(Messages.IncorrectData);

        }
    }
}
