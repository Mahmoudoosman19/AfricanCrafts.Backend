using Product.Application.Features.Advertisements.Queries.CustomerGetAllAdvertisment;

namespace Product.Application.Specifications.Advertisement
{
    public class CustomerGetAdvertisementByStatusSpecification : Specification<Domain.Entities.Advertisement>
    {
        public CustomerGetAdvertisementByStatusSpecification(CustomerGetAdvertisementByStatusQuery request)
        {
            ApplyPaging(request.PageSize, request.PageIndex);

            AddCriteria(c => c.IsActive == true);

            if (request.IsActive is not null)
            {
                AddCriteria(c => c.IsActive == request.IsActive);
            }
        }
    }
}
