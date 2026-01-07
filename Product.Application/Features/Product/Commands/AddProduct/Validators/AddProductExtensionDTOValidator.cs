using Product.Application.Features.Product.Commands.AddProduct.DTOs;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.Commands.AddProduct.Validators
{
    internal class AddProductExtensionDTOValidator : AbstractValidator<AddProductExtensionDTO>
    {
        private readonly IGenericRepository<Size> _sizeRepo;

        public AddProductExtensionDTOValidator(IGenericRepository<Size> sizeRepo, IGenericRepository<Domain.Entities.Color> colorRepo)
        {
            _sizeRepo = sizeRepo;

            RuleFor(exe => exe.SizeId)
                    .NotNull().WithMessage(Messages.EmptyField)
                    .EntityExist(sizeRepo).WithMessage(Messages.NotFound)
                    .MustAsync(SizeRelatedToGroup).WithMessage(Messages.IncorrectData);

            RuleFor(exe => exe.ColorCode)
                    .NotNull().WithMessage(Messages.EmptyField).
                    Must(code => code.StartsWith('#')).WithMessage(Messages.IncorrectData);


            RuleFor(exe => exe.Amount)
                    .NotNull().WithMessage(Messages.EmptyField)
                    .GreaterThan(0).When(prod => !prod.IsDeleted).WithMessage(Messages.IncorrectData);
            RuleFor(exe => exe.Fees.Value)
               .GreaterThanOrEqualTo(0).When(ext => ext.Fees.HasValue).WithMessage(Messages.IncorrectData);
        }

        private async Task<bool> SizeRelatedToGroup(Guid sizeId, CancellationToken cancellationToken)
            => await _sizeRepo.IsExistAsync(size => size.Id == sizeId && size.SizeGroupId != Guid.Empty, cancellationToken);
    }
}
