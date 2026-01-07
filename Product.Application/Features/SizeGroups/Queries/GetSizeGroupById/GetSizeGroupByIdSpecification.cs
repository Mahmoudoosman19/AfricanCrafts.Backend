using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Queries.GetSizeGroupById
{
    internal class GetSizeGroupByIdSpecification : Specification<SizeGroup>
    {
        public GetSizeGroupByIdSpecification(GetSizeGroupByIdQuery request)
        {
            AddCriteria(x => x.Id == request.Id);
            AddInclude(nameof(SizeGroup.Sizes));
            AddInclude(nameof(SizeGroup.SizeGroupQuestions));
        }
    }
}
