using Product.Application.SharedDTOs.SizeGroupQuestion;

namespace Product.Application.Features.SizeGroups.Commands.CreateSizeGroup;

public sealed class SizeGroupQuestionDtoValidator
    : AbstractValidator<CreateSizeGroupQuestionDto>
{
    public SizeGroupQuestionDtoValidator()
    {
        RuleFor(x => x.QuestionAr)
            .NotEmpty()
            .WithMessage(Messages.EmptyField);

        RuleFor(x => x.QuestionEn)
            .NotEmpty()
            .WithMessage(Messages.EmptyField);

        RuleFor(x => x.QuestionAr)
            .IsArabic(combineNumbers: true).WithMessage(Messages.IncorrectData)
            .When(x => !string.IsNullOrEmpty(x.QuestionAr));

        RuleFor(x => x.QuestionEn)
            .IsEnglish(combineNumbers: true).WithMessage(Messages.IncorrectData)
            .When(x => !string.IsNullOrEmpty(x.QuestionEn));
    }
}