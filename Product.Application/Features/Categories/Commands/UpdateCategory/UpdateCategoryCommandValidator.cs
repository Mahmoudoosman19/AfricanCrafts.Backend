using Product.Application.Helpers;
using Product.Domain.Entities;

namespace Product.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IGenericRepository<SizeGroup> _sizeGroupRepo;
        public UpdateCategoryCommandValidator(IGenericRepository<Category> categoryRepo, IGenericRepository<SizeGroup> sizeGroupRepo)
        {
            _categoryRepo = categoryRepo;
            _sizeGroupRepo = sizeGroupRepo;
            ValidationRules();
        }
        private void ValidationRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EntityExist(_categoryRepo).WithMessage(Messages.NotFound);

            RuleFor(x => x).CustomAsync(IsNameExistForAnotherCategoryAsync);
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .MaximumLength(50).WithMessage(Messages.CategoryNameMaxLength)
                .IsArabic().WithMessage(Messages.IncorrectData);


            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .MaximumLength(50).WithMessage(Messages.CategoryNameMaxLength)
                  .IsEnglish().WithMessage(Messages.IncorrectData); ;

            RuleFor(x => x).Custom(ValidateImage);
            RuleFor(x => x.SizeGroupId)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EntityExist(_sizeGroupRepo).WithMessage(Messages.NotFound);

            RuleFor(x => x.ParentId.Value)
                 .EntityExist(_categoryRepo).When(category => category.ParentId.HasValue)
                 .WithMessage(Messages.NotFound);
        }

        private async Task IsNameExistForAnotherCategoryAsync(UpdateCategoryCommand request, ValidationContext<UpdateCategoryCommand> context, CancellationToken cancellationToken)
        {
            var isExist = await _categoryRepo.IsExistAsync(x => x.NameAr == request.NameAr && x.Id != request.Id);
            if (isExist)
                context.AddFailure(nameof(request.NameAr), Messages.RedundantData);
            isExist = await _categoryRepo.IsExistAsync(x => x.NameEn == request.NameEn && x.Id != request.Id);
            if (isExist)
                context.AddFailure(nameof(request.NameEn), Messages.RedundantData);
        }

        private void ValidateImage(UpdateCategoryCommand request, ValidationContext<UpdateCategoryCommand> context)
        {
            if (request.Image != null && !ImageSetting.IsAllowedImageTypes(request.Image.FileName))
                context.AddFailure(nameof(request.Image), Messages.AllowedImageTypes.Replace("allTypes", ImageSetting.allowedImageTypes));

            if (request.Image != null && request.Image.Length > ImageSetting.MaxLegthByBytes(5))
                context.AddFailure(nameof(request.Image), Messages.MaxSizeFiles);
        }

    }
}
