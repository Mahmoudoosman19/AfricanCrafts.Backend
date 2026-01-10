using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroupQuestions.Queries.GetListSizeGroupQuestion
{
    public class GetListSizeQuestionValidator : AbstractValidator<GetListSizeQuestionQuery>
    {
        public GetListSizeQuestionValidator(IProductRepository<SizeGroup> sizeGroupRepo)
        {

            RuleFor(x => x.SizeGroupId.Value)
               .EntityExist(sizeGroupRepo).When(sizeGroup => sizeGroup.SizeGroupId.HasValue).WithMessage(Messages.NotFound);
        }
    }
}
