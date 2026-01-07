using IdentityHelper.Abstraction;
using Product.Application.Specifications.Products;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.Commands.AddProduct.Validators
{
    internal class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
        private readonly ITokenExtractor _userManager;

        public AddProductCommandValidator(
            IGenericRepository<Domain.Entities.Product> productRepo,
            IGenericRepository<Category> categoryRepo,
            IGenericRepository<Point> pointRepo,
            IGenericRepository<Domain.Entities.Color> colorRepo,
            IGenericRepository<Size> sizeRepo,
            ITokenExtractor userManager)
        {
            _productRepo = productRepo;
            _userManager = userManager;

            RuleFor(product => product)
                .MustAsync(IsProductUnique).WithMessage(Messages.RedundantData);
            
            RuleFor(product => product)
                .MustAsync(IsProductCodeUnique).WithMessage(Messages.RedundantData);

            RuleFor(product => product.NameAr)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsArabic(combineNumbers: true).WithMessage(Messages.IncorrectData);

            RuleFor(product => product.NameEn)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsEnglish(combineNumbers: true).WithMessage(Messages.IncorrectData);

            RuleFor(product => product.DescriptionAr)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsArabic(true).WithMessage(Messages.IncorrectData); ;

            RuleFor(product => product.DescriptionEn)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsEnglish(true).WithMessage(Messages.IncorrectData); ;

            RuleFor(product => product.Price)
                .NotNull().WithMessage(Messages.EmptyField)
                .GreaterThan(0).WithMessage(Messages.IncorrectData);

            RuleFor(product => product.CategoryId)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EntityExist(categoryRepo).WithMessage(Messages.NotFound);

            RuleFor(product => product.PointsId.Value)
                .EntityExist(pointRepo).When(product => product.PointsId.HasValue).WithMessage(Messages.NotFound);

            RuleFor(product => product.Images)
                .ListMustContainMoreThan().WithMessage(Messages.EmptyField)
                .ForEachSetValidator(new AddProductImageDTOValidator(colorRepo));

            RuleFor(product => product.Extensions)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .ListMustContainMoreThan().WithMessage(Messages.EmptyField)
                .ForEachSetValidator(new AddProductExtensionDTOValidator(sizeRepo, colorRepo));
        }

        private async Task<bool> IsProductUnique(AddProductCommand product, CancellationToken cancellationToken)
        {
            var result = !(await _productRepo.IsExistAsync(p =>
             (p.NameAr == product.NameAr || p.NameEn == product.NameEn)
             && p.CategoryId == product.CategoryId
             && p.VendorId == _userManager.GetUserId()
             , cancellationToken));
            return result;
        }
        private async Task<bool> IsProductCodeUnique(AddProductCommand product, CancellationToken cancellationToken)
        {
            if (product.ProductCode == null)
                return true;

            var result =  _productRepo.GetEntityWithSpec(new GetProductsByStatusAndVendorIdAndProductCodeAndNameWithImageSpecification(product.ProductCode));
           if (result == null)
                return false;
            return true;
        }

    }
}
