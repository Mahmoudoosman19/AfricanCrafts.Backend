using Product.Application.Features.Coupons.Query;

namespace Product.Application.Features.Coupon.AdminGenerateCoupons.Specifications
{
    public class GetListCouponSpecification : Specification<Domain.Entities.Coupon>
    {
        public GetListCouponSpecification(GetAllCouponsQuery request)
        {
            ApplyPaging(request.PageSize, request.PageIndex);
        }

    }
}