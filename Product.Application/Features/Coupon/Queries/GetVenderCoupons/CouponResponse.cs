namespace Product.Application.Features.Coupon.Queries.ListCoupon
{
    public class CouponResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int UserCount { get; set; }
        public double DiscountPercentage { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public Guid UserId { get; set; }
    }
}
