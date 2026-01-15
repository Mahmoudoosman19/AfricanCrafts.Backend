using FluentValidation;
using IdentityHelper.Abstraction;
using Product.Application.Specifications.Products;
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.Commands.AddProduct.Validators
{
    internal class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        private readonly IProductRepository<Domain.Entities.Product> _productRepo;
        private readonly IProductRepository<Domain.Entities.Size> _sizeRepo;

        public AddProductCommandValidator(
            IProductRepository<Domain.Entities.Product> productRepo,
            IProductRepository<Category> categoryRepo,
            IProductRepository<Domain.Entities.Size> sizeRepo)
        {
            _productRepo = productRepo;
            _sizeRepo = sizeRepo; 

            RuleFor(x => x).MustAsync(IsProductUnique).WithMessage(Messages.RedundantData);
            RuleFor(x => x).MustAsync(IsProductCodeUnique).WithMessage(Messages.RedundantData);

            RuleFor(x => x.NameAr).NotEmpty().IsArabic(true);
            RuleFor(x => x.NameEn).NotEmpty().IsEnglish(true);

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .EntityExist(categoryRepo).WithMessage(Messages.NotFound);

            // فحص الصور
            RuleFor(x => x.Images).NotEmpty().Must(list => list.Count > 0);
            RuleForEach(x => x.Images).ChildRules(image => {
                image.RuleFor(i => i.ImageFile).NotNull().WithMessage(Messages.EmptyField);
                image.RuleFor(i => i.ColorCode).NotEmpty()
                    .Must(code => code.StartsWith('#')).WithMessage(Messages.IncorrectData);
            });

            // فحص الـ Extensions
            RuleFor(x => x.Extensions).NotEmpty();
            RuleForEach(x => x.Extensions).ChildRules(ext => {
                ext.RuleFor(e => e.Amount).GreaterThanOrEqualTo(0).WithMessage(Messages.IncorrectData);
                ext.RuleFor(e => e.Fees).GreaterThanOrEqualTo(0);
                ext.RuleFor(e => e.SizeId)
                .NotNull().WithMessage(Messages.EmptyField) 
                .MustAsync(SizeRelatedToGroup);
            });
        }

        private async Task<bool> IsProductUnique(AddProductCommand command, CancellationToken token)
        {
            return !await _productRepo.IsExistAsync(p =>
                (p.NameAr == command.NameAr || p.NameEn == command.NameEn)
                && p.CategoryId == command.CategoryId, token);
        }

        private async Task<bool> IsProductCodeUnique(AddProductCommand command, CancellationToken token)
        {
            if (string.IsNullOrEmpty(command.ProductCode)) return true;

            var spec = new GetProductsByStatusAndProductCodeAndNameWithImageSpecification(command.ProductCode);
            var existingProduct =  _productRepo.GetEntityWithSpec(spec);

            return existingProduct == null;
        }

        private async Task<bool> SizeRelatedToGroup(Guid? sizeId, CancellationToken cancellationToken)
        {
            if (!sizeId.HasValue) return false;
            return await _sizeRepo.IsExistAsync(size => size.Id == sizeId && size.SizeGroupId != Guid.Empty, cancellationToken);
        }
    }
}
