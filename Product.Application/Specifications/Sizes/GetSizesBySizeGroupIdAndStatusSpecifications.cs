using Product.Application.Features.Sizes.Queries.GetSizes;
using Product.Domain.Entities;

namespace Product.Application.Specifications.Sizes
{
    internal class GetSizesBySizeGroupIdAndStatusSpecifications : Specification<Size>
    {
        public GetSizesBySizeGroupIdAndStatusSpecifications(Guid sizeGroupId)
        {
            AddCriteria(x => x.SizeGroupId == sizeGroupId);
        }
        public GetSizesBySizeGroupIdAndStatusSpecifications(GetSizesByStatusQuery request)
        {
            if (request.IsActive is not null)
                AddCriteria(c => c.IsActive == request.IsActive);
            ApplyPaging(request.PageSize, request.PageIndex);
        }
    }
}
