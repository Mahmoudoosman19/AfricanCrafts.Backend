using Product.Domain.Entities;

namespace Product.Application.Specifications.SizeGroups
{
    public sealed class DeleteSizesByGroupIdSpecification : Specification<Size>
    {
        public DeleteSizesByGroupIdSpecification(Guid sizeGroupId)
        {
            AddCriteria(x => x.SizeGroupId == sizeGroupId);
        }
    }
}
