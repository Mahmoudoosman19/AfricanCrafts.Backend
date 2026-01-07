using IdentityHelper.Abstraction;
using Product.Application.Features.Product.Commands.AddProduct.Validators;
using Product.Application.Specifications.Products;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.Commands.SuperVisorAddProduct.Validators;

internal class SuperVisorAddProductCommandValidator : AbstractValidator<SuperVisorAddProductCommand>
{
    private readonly IGenericRepository<Domain.Entities.Product> _productRepo;

    public SuperVisorAddProductCommandValidator(
        IGenericRepository<Domain.Entities.Product> productRepo,
        IGenericRepository<Category> categoryRepo,
        IGenericRepository<Point> pointRepo,
        IGenericRepository<Domain.Entities.Color> colorRepo,
        IGenericRepository<Size> sizeRepo,
        IUserManagement userManager)
    {
        _productRepo = productRepo;

        RuleFor(product => product)
            .MustAsync(IsProductUnique).WithMessage(Messages.RedundantData);
        RuleFor(product => product)
         .MustAsync(IsProductCodeUnique).WithMessage(Messages.RedundantData);
        RuleFor(product => product.VendorId)
            .MustAsync(async (vendorId, _)
                =>
            {
                var vendor = await userManager.GetUserData(vendorId);
                return vendor != null && vendor.Role == "Vendor";
            })
            .WithMessage(Messages.NotFound);

        RuleFor(product => product.NameAr)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .IsArabic(combineNumbers: true).WithMessage(Messages.IncorrectData);

        RuleFor(product => product.NameEn)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .IsEnglish(combineNumbers: true).WithMessage(Messages.IncorrectData);

        RuleFor(product => product.DescriptionAr)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .IsArabic().WithMessage(Messages.IncorrectData);

        RuleFor(product => product.DescriptionEn)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .IsEnglish().WithMessage(Messages.IncorrectData);

        RuleFor(product => product.Price)
            .NotNull().WithMessage(Messages.EmptyField)
            .GreaterThan(0).WithMessage(Messages.IncorrectData);

        RuleFor(product => product.CategoryId)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .EntityExist(categoryRepo).WithMessage(Messages.NotFound);

        RuleFor(product => product.PointsId!.Value)
            .EntityExist(pointRepo).When(product => product.PointsId.HasValue).WithMessage(Messages.NotFound);

        RuleFor(product => product.Images)
            .ListMustContainMoreThan().WithMessage(Messages.EmptyField)
            .ForEachSetValidator(new AddProductImageDTOValidator(colorRepo));

        RuleFor(product => product.Extensions)
            .ListMustContainMoreThan().WithMessage(Messages.EmptyField)
            .ForEachSetValidator(new AddProductExtensionDTOValidator(sizeRepo, colorRepo));
    }

    private async Task<bool> IsProductUnique(SuperVisorAddProductCommand product, CancellationToken cancellationToken)
    {
        var result = !(await _productRepo.IsExistAsync(p =>
                (p.NameAr == product.NameAr || p.NameEn == product.NameEn)
                && p.CategoryId == product.CategoryId
                && p.VendorId == product.VendorId
            , cancellationToken));
        return result;
    }
    private async Task<bool> IsProductCodeUnique(SuperVisorAddProductCommand product, CancellationToken cancellationToken)
    {
        if (product.ProductCode == null)
            return true;

        var result = _productRepo.GetEntityWithSpec(new GetProductsByStatusAndVendorIdAndProductCodeAndNameWithImageSpecification(product.ProductCode));
        if (result == null)
            return false;
        return true;
    }
}