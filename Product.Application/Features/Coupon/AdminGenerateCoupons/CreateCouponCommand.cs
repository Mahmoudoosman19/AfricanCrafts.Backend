using Product.Application.Features.Coupon.Dto;

namespace Product.Application.Features.Coupon.AdminGenerateCoupons
{
    public class CreateCouponCommand : ICommand
    {
        public int UserCount { get; set; }
        public double DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public Guid UserId { get;  set; }
        public List<Guid> ?ProductIds { get; set; }
        public int NumOfCoupons { get; set; } = 1;
    }
}
