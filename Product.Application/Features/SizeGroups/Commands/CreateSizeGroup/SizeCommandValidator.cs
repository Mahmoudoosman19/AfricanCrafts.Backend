using Product.Application.SharedDTOs.SizeGroup;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Commands.CreateSizeGroup
{
    public sealed class SizeCommandValidator
        : AbstractValidator<CreateSizeDto>
    {
        public SizeCommandValidator(
            IGenericRepository<Size> sizeRepository)
        {
            this.ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsArabic(combineNumbers: true).WithMessage(Messages.IncorrectData)
                .When(x => !string.IsNullOrEmpty(x.NameAr),
                    ApplyConditionTo.CurrentValidator);

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsEnglish(combineNumbers: true).WithMessage(Messages.IncorrectData)
                .When(x => !string.IsNullOrEmpty(x.NameEn),
                    ApplyConditionTo.CurrentValidator);

            RuleFor(x => x.DescriptionAr)
                .IsArabic(combineNumbers: true).WithMessage(Messages.IncorrectData)
                .When(x => !string.IsNullOrEmpty(x.DescriptionAr));

            RuleFor(x => x.DescriptionEn)
                .IsEnglish(combineNumbers: true).WithMessage(Messages.IncorrectData)
                .When(x => !string.IsNullOrEmpty(x.DescriptionEn));
        }
    }
}