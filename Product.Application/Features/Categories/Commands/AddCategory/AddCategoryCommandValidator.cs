using Product.Application.Helpers;
using Product.Domain.Entities;

namespace Product.Application.Features.Categories.Commands.AddCategory
{
    internal class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IGenericRepository<SizeGroup> _sizeGroupRepo;
        public AddCategoryCommandValidator(IGenericRepository<Category> categoryRepo, IGenericRepository<SizeGroup> sizeGroupRepo)
        {
            _categoryRepo = categoryRepo;
            _sizeGroupRepo = sizeGroupRepo;

            ValidationRules();
        }
        private void ValidationRules()
        {
            RuleFor(x => x).CustomAsync(IsNameExist);
            RuleFor(x => x.NameAr)
                 .NotEmpty().WithMessage(Messages.NotFound)
                 .MaximumLength(50).WithMessage(Messages.CategoryNameMaxLength)
                 .IsArabic().WithMessage(Messages.IncorrectData);

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .MaximumLength(50).WithMessage(Messages.CategoryNameMaxLength)
                .IsEnglish().WithMessage(Messages.IncorrectData);

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .Must((Key, CancellationToken) => ImageSetting.IsAllowedImageTypes(Key.Image.FileName)).WithMessage(Messages.AllowedImageTypes.Replace("allTypes", ImageSetting.allowedImageTypes));

            RuleFor(x => x.Image.Length)
                .LessThanOrEqualTo(ImageSetting.MaxLegthByBytes(5)).WithMessage(Messages.MaxSizeFiles);

            RuleFor(x => x.SizeGroupId)
                .NotEmpty().WithMessage(Messages.EmptyField)
               .EntityExist(_sizeGroupRepo).WithMessage(Messages.NotFound);

            RuleFor(x => x.ParentId.Value)
                .EntityExist(_categoryRepo).When(category => category.ParentId.HasValue)
                .WithMessage(Messages.NotFound);
        }


        private async Task IsNameExist(AddCategoryCommand request, ValidationContext<AddCategoryCommand> context, CancellationToken cancellationToken)
        {
            var isExist = await _categoryRepo.IsExistAsync(x => x.NameAr == request.NameAr);
            if (isExist)
                context.AddFailure(nameof(request.NameAr), Messages.RedundantData);
            isExist = await _categoryRepo.IsExistAsync(x => x.NameEn == request.NameEn);
            if (isExist)
                context.AddFailure(nameof(request.NameEn), Messages.RedundantData);

        }

    }
}


