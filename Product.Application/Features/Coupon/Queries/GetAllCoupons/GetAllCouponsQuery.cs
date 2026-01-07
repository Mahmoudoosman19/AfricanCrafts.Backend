using Product.Application.Features.Coupon.Dto;

namespace Product.Application.Features.Coupons.Query
{
    public class GetAllCouponsQuery : IQuery<List<CouponDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
       
        public bool? IsActive { get; set; }
        public string? Code { get; set; }
    }
}
