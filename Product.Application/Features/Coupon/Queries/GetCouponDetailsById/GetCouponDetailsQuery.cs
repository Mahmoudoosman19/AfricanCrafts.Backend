
using Product.Application.Features.Coupon.Dto;

namespace Product.Application.Features.Coupon.Queries.GetCouponDetailsById
{
    public class GetCouponDetailsQuery: IQuery<CouponDto>
    {
        public Guid Id { get; set; }
    }
}
