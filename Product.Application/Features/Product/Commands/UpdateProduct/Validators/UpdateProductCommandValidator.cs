using IdentityHelper.Abstraction;
using Product.Application.Features.Product.Commands.UpdateProduct.DTOs;
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.Commands.UpdateProduct.Validators
{
    internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IProductRepository<Domain.Entities.Product> _productRepo;
        private readonly ITokenExtractor _userManager;

        public UpdateProductCommandValidator(IProductRepository<Domain.Entities.Product> productRepo, IProductRepository<ProductImage> productImageRepo, IProductRepository<ProductExtension> productExtensionRepo,
            IProductRepository<Category> categoryRepo, IProductRepository<Domain.Entities.Color> colorRepo, IProductRepository<Size> sizeRepo, ITokenExtractor userManager)
        {
            _productRepo = productRepo;
            _userManager = userManager;

            RuleFor(product => product)
               .MustAsync(IsProductUnique).WithMessage(Messages.RedundantData);

            RuleFor(product => product.Id)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EntityExist(productRepo).WithMessage(Messages.NotFound);

            RuleFor(product => product.NameAr)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsArabic().WithMessage(Messages.IncorrectData);

            RuleFor(product => product.NameEn)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsEnglish().WithMessage(Messages.IncorrectData);

            RuleFor(product => product.DescriptionAr)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsArabic(true).WithMessage(Messages.IncorrectData);

            RuleFor(product => product.DescriptionEn)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsEnglish(true).WithMessage(Messages.IncorrectData);

            RuleFor(product => product.Price)
                .NotNull().WithMessage(Messages.EmptyField)
                .GreaterThan(0).WithMessage(Messages.IncorrectData);

            RuleFor(product => product.CategoryId)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EntityExist(categoryRepo).WithMessage(Messages.NotFound);

          
            RuleFor(product => product.Images)
                .ForEachSetValidator(new UpdateProductImageDTOValidator(productImageRepo, colorRepo))
                .DependentRules(() =>
                {
                    RuleFor(product => new { product.Images, product.Id })
                    .MustAsync((product, cancellationToken) => ImagesUnderThisProduct(productImageRepo, product.Images, product.Id, cancellationToken))
                    .WithName("Product Image").WithMessage(Messages.IncorrectData);
                });

            RuleFor(product => product.Extensions)
                .ForEachSetValidator(new UpdateProductExtensionDTOValidator(productExtensionRepo, sizeRepo, colorRepo))
                .DependentRules(() =>
                {
                    RuleFor(product => new { product.Extensions, product.Id })
                    .MustAsync((product, cancellationToken) => ExtensionsUnderThisProduct(productExtensionRepo, product.Extensions, product.Id, cancellationToken))
                    .WithName("Product Extension").WithMessage(Messages.IncorrectData);
                });
        }

        private async Task<bool> ImagesUnderThisProduct(IProductRepository<ProductImage> productImageRepo, List<UpdateProductImageDTO> dtos, Guid productId, CancellationToken cancellationToken)
        {
            var dtoIds = dtos.Where(dto => dto.Id != null).Select(dto => dto.Id).ToList();
            return !await productImageRepo.IsExistAsync(img => dtoIds.Contains(img.Id) && img.ProductId != productId, cancellationToken);
        }

        private async Task<bool> ExtensionsUnderThisProduct(IProductRepository<ProductExtension> productExtensionRepo, List<UpdateProductExtensionDTO> dtos, Guid productId, CancellationToken cancellationToken)
        {
            var dtoIds = dtos.Where(dto => dto.Id != null).Select(dto => dto.Id).ToList();
            return !await productExtensionRepo.IsExistAsync(exe => dtoIds.Contains(exe.Id) && exe.ProductId != productId, cancellationToken);
        }

        private async Task<bool> IsProductUnique(UpdateProductCommand product, CancellationToken cancellationToken)
            => !(await _productRepo.IsExistAsync(p =>
            (p.NameAr == product.NameAr || p.NameEn == product.NameEn)
            && p.CategoryId == product.CategoryId
            && p.Id != product.Id
            , cancellationToken));
    }
}
