using Product.Application.Features.Advertisements.Queries.GetAllAdvertisment;

namespace Product.Application.Specifications.Advertisement
{
    public class GetAllAdvertisementImageByStatusSpecification : Specification<Domain.Entities.Advertisement>
    {
        public GetAllAdvertisementImageByStatusSpecification(GetAllAdvertisementImageByStatusQuery request)
        {
            ApplyPaging(request.PageSize, request.PageIndex);
            if (request.IsActive is not null)
                AddCriteria(c => c.IsActive == request.IsActive);
        }
    }
}
