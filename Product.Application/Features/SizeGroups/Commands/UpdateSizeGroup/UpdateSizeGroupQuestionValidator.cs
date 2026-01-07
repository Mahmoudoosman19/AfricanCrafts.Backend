namespace Product.Application.Features.SizeGroups.Commands.UpdateSizeGroup;

public class UpdateSizeGroupQuestionValidator
    : AbstractValidator<UpdateSizeGroupQuestionDto>
{
    public UpdateSizeGroupQuestionValidator()
    {
        RuleFor(x => x.QuestionAr)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .IsArabic(combineNumbers: true).WithMessage(Messages.IncorrectData)
            .When(x => !string.IsNullOrEmpty(x.QuestionAr),
                ApplyConditionTo.CurrentValidator);

        RuleFor(x => x.QuestionEn)
            .NotEmpty().WithMessage(Messages.EmptyField)

            .IsEnglish(combineNumbers: true).WithMessage(Messages.IncorrectData)
            .When(x => !string.IsNullOrEmpty(x.QuestionEn),
                ApplyConditionTo.CurrentValidator);
    }
}