using Product.Application.Features.Advertisements.Queries.GetList;

namespace Product.Application.Specifications.Advertisement
{
    public class FilterAdvertisementBaseStatusAndRoleSpecification : Specification<Domain.Entities.Advertisement>
    {
        public FilterAdvertisementBaseStatusAndRoleSpecification(GetAdvertisementBaseStatusAndRoleQuery request, bool isAdmin)
        {
            ApplyPaging(request.PageSize, request.PageIndex);

            if (request.IsActive is not null)
            {
                AddCriteria(c => c.IsActive == request.IsActive);
            }

            if (!isAdmin)
            {
                AddCriteria(c => c.IsActive == true);
            }
        }
    }
}
