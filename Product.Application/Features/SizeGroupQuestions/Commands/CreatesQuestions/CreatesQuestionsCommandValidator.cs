using Product.Domain.Entities;

namespace Product.Application.Features.SizeTable.Commands.CreatesSizeTable
{
    public class CreatesQuestionsCommandValidator: AbstractValidator<CreatesQuestionsCommand>
    {
        private readonly IGenericRepository<SizeGroupQuestion> _questionsRepo;

        public CreatesQuestionsCommandValidator(IGenericRepository<SizeGroupQuestion> questionsRepo,
            IGenericRepository<SizeGroup> sizeGroupRepo)
        {
            _questionsRepo = questionsRepo;

            RuleFor(x=>x.SizeGroupId).NotEmpty()
               .WithMessage(Messages.EmptyField)
               .EntityExist(sizeGroupRepo).WithMessage(Messages.NotFound);

            RuleFor(x => x.Questions)
           .Must(x => x.Count() > 0)
           .WithMessage(Messages.NotFound);

            RuleFor(x => x)
           .CustomAsync(IsQuestionExist);



        }

        private async Task IsQuestionExist(CreatesQuestionsCommand request, ValidationContext<CreatesQuestionsCommand> context, CancellationToken cancellationToken)
        {
            var questionsEr=request.Questions.Select(x=>x.QuestionEn).ToList();
            var questionsAr = request.Questions.Select(x => x.QuestionAr).ToList();
            var questionExists =await _questionsRepo.IsExistAsync(
                x=>((questionsAr.Contains(x.QuestionAr))||(questionsEr.Contains(x.QuestionEn)))
                &&x.SizeGroupId==request.SizeGroupId
                );

            if (questionExists)
                context.AddFailure("Question", Messages.RedundantData);

        }


    }
}

