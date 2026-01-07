using Product.Application.Features.Coupons.Query;

namespace Product.Application.Features.Coupon.AdminGenerateCoupons.Specifications
{
    public class GetCouponsOrderByDateSpecification : Specification<Domain.Entities.Coupon>
    {
        public GetCouponsOrderByDateSpecification(GetAllCouponsQuery request)
        {
            
            AddOrderBy(x => x.CreatedOnUtc);
            ApplyPaging(request.PageSize, request.PageIndex);
            if (request.IsActive != null)
            {
                AddCriteria(x => x.IsActive == request.IsActive);
            }
            if (request.Code != null)
            {
                AddCriteria(x => x.Code.Contains(request.Code));
            }
        }
    }
}
