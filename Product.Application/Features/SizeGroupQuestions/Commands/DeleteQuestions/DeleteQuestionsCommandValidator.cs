using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroupQuestions.Commands.DeleteQuestions
{
    public class DeleteQuestionsCommandValidator : AbstractValidator<DeleteQuestionCommand>
    {
        public DeleteQuestionsCommandValidator(IProductRepository<SizeGroupQuestion> questionsRepo)
        {
            RuleFor(x => x.SizeGroupQuestionId)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .EntityExist(questionsRepo)
                .WithMessage(Messages.NotFound);
        }
    }
}
