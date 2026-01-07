using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Commands.UpdateSizeGroup
{
    public sealed class UpdateSizeValidator
        : AbstractValidator<UpdateSizeDto>
    {
        private readonly IGenericRepository<Size> _sizeRepository;

        public UpdateSizeValidator(
            IGenericRepository<Size> sizeRepository)
        {
            _sizeRepository = sizeRepository;
            this.ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage(Messages.EmptyField);

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