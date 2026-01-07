using Product.Application.SharedDTOs.CreatesQuestions;
using Product.Domain.Entities;

namespace Product.Application.Features.CreatesَََQuestions.Commands.CreatesQuestions
{
    public class CreatesQuestionsValidator: AbstractValidator<CreatesQuestionsDto>
    {
       
        public CreatesQuestionsValidator(IGenericRepository<SizeGroupQuestion> sizeQestionRepo)
        {

            RuleFor(x => x .QuestionAr)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .IsArabic()
                .WithMessage(Messages.IncorrectData);
            RuleFor ( x=> x .QuestionEn)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .IsEnglish()
                .WithMessage(Messages.IncorrectData);
            
        }

    }
}
