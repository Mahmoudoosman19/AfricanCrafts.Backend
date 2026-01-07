using Product.Application.Features.Coupon.Dto;

namespace Product.Application.Features.Coupon.Queries.GetCouponDetailsByCodeForCustomer
{
    public class GetCouponDetailsByCodeForCustomerQuery : IQuery<CouponDto>
    {
        public string Code { get; set; }
    }
}
