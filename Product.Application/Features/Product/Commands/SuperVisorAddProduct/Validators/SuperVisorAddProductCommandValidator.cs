using IdentityHelper.Abstraction;
using Product.Application.Features.Product.Commands.AddProduct.Validators;
using Product.Application.Specifications.Products;
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.Commands.SuperVisorAddProduct.Validators;

internal class SupervisorAddProductCommandValidator : AbstractValidator<SupervisorAddProductCommand>
{
    private readonly IProductRepository<Domain.Entities.Product> _productRepo;
    private readonly IProductRepository<Domain.Entities.Size> _sizeRepo;

    public SupervisorAddProductCommandValidator(
        IProductRepository<Domain.Entities.Product> productRepo,
        IProductRepository<Category> categoryRepo,
        IProductRepository<Domain.Entities.Color> colorRepo,
        IProductRepository<Size> sizeRepo,
        IUserManagement userManager)
    {
        _productRepo = productRepo;
        _sizeRepo= sizeRepo;
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
        RuleFor(x => x.Images).NotEmpty().Must(list => list.Count > 0);
        RuleForEach(x => x.Images).ChildRules(image => {
            image.RuleFor(i => i.ImageFile).NotNull().WithMessage(Messages.EmptyField);
            image.RuleFor(i => i.ColorCode).NotEmpty()
                .Must(code => code.StartsWith('#')).WithMessage(Messages.IncorrectData);
        });

        RuleFor(x => x.Extensions).NotEmpty();
        RuleForEach(x => x.Extensions).ChildRules(ext => {
            ext.RuleFor(e => e.Amount).GreaterThanOrEqualTo(0).WithMessage(Messages.IncorrectData);
            ext.RuleFor(e => e.Fees).GreaterThanOrEqualTo(0);
            ext.RuleFor(e => e.SizeId)
            .NotNull().WithMessage(Messages.EmptyField) 
            .MustAsync(SizeRelatedToGroup);
        });


    }
    private async Task<bool> SizeRelatedToGroup(Guid? sizeId, CancellationToken cancellationToken)
    {
        if (!sizeId.HasValue) return false;
        return await _sizeRepo.IsExistAsync(size => size.Id == sizeId && size.SizeGroupId != Guid.Empty, cancellationToken);
    }
    private async Task<bool> IsProductUnique(SupervisorAddProductCommand product, CancellationToken cancellationToken)
    {
        var result = !(await _productRepo.IsExistAsync(p =>
                (p.NameAr == product.NameAr || p.NameEn == product.NameEn)
                && p.CategoryId == product.CategoryId
            , cancellationToken));
        return result;
    }
    private async Task<bool> IsProductCodeUnique(SupervisorAddProductCommand product, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(product.ProductCode))
            return true;
        var existingProduct = _productRepo
            .GetEntityWithSpec(new GetProductsByStatusAndProductCodeAndNameWithImageSpecification(product.ProductCode));

        return existingProduct == null;
    }
}