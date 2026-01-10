using Product.Application.Features.Product.Commands.UpdateProduct.DTOs;
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.Commands.UpdateProduct.Validators
{
    internal class UpdateProductExtensionDTOValidator : AbstractValidator<UpdateProductExtensionDTO>
    {
        public UpdateProductExtensionDTOValidator(IProductRepository<ProductExtension> productExtensionRepo,
            IProductRepository<Size> sizeRepo, IProductRepository<Domain.Entities.Color> colorRepo)
        {
            When(exe => exe.Id != Guid.Empty, () =>
            {
                RuleFor(exe => exe.Id)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EntityExist(productExtensionRepo).WithMessage(Messages.NotFound);
            });

            RuleFor(exe => exe.SizeId)
                .NotNull().WithMessage(Messages.EmptyField)
                .EntityExist(sizeRepo).WithMessage(Messages.NotFound);

            RuleFor(exe => exe.ColorCode)
                .NotNull().WithMessage(Messages.EmptyField)
                .Must(code => code.StartsWith('#')).WithMessage(Messages.IncorrectData);

            RuleFor(exe => exe.Amount)
                .NotNull().WithMessage(Messages.EmptyField)
                .GreaterThan(0).When(prod => !prod.IsDeleted).WithMessage(Messages.IncorrectData);

            RuleFor(exe => exe.Fees.Value)
                .GreaterThanOrEqualTo(0).When(ext => ext.Fees.HasValue).WithMessage(Messages.IncorrectData);
        }
    }
}
