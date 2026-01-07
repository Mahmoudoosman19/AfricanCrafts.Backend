using Product.Application.Features.SizeGroupQuestions.Queries.GetListSizeGroupQuestion;

namespace Product.Application.Specifications.SizeGroupQuestion
{
    public class SizeQuestionBySizeGroupIdWithSizeGroupSpecification : Specification<Domain.Entities.SizeGroupQuestion>
    {
        public SizeQuestionBySizeGroupIdWithSizeGroupSpecification(GetListSizeQuestionQuery request)
        {
            if (request.SizeGroupId.HasValue)
                AddCriteria(c => c.SizeGroupId == request.SizeGroupId);
            AddCriteria(c => c.IsDeleted == false);
            ApplyPaging(request.PageSize, request.PageIndex);
            AddInclude(nameof(Domain.Entities.SizeGroupQuestion.SizeGroup));
        }
    }
}
