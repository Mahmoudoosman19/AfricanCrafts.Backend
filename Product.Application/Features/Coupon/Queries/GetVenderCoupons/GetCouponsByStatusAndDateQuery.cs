namespace Product.Application.Features.Coupon.Queries.ListCoupon
{
    public class GetCouponsByStatusAndDateQuery : IQuery<IEnumerable<CouponResponse>>
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        public string? Code { get; set; }
        public double? DiscountPercentage { get; set; }
        public bool? IsActive { get; set; }
    }
}
