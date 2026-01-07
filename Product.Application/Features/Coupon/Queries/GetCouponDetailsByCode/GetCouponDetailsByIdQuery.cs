using Product.Application.Features.Coupon.Dto;

namespace Product.Application.Features.Coupon.Queries.GetCouponDetailsByCode
{
    public class GetCouponDetailsByIdQuery:IQuery<CouponDto>
    {
        public string Code {  get; set; }
    }
}
