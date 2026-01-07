using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Commands.UpdateSizeGroup
{
    internal class GetSizeGroupWithSizeSpecification : Specification<SizeGroup>
    {
        public GetSizeGroupWithSizeSpecification(Guid id)
        {
            AddCriteria(x => x.Id == id);
            AddInclude(nameof(SizeGroup.Sizes));
            AddInclude(nameof(SizeGroup.SizeGroupQuestions));
        }
    }
}
