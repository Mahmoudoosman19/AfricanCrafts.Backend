using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class CouponProducts : Entity<Guid>, IAuditableEntity
    {
        public Guid ProductId { get; private set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        public Guid CouponId { get; private set; }
        [ForeignKey(nameof(CouponId))]
        public virtual Coupon Coupon { get; set; }
        public DateTime CreatedOnUtc { get ; set ; }
        public DateTime? ModifiedOnUtc { get ; set ; }

        public void SetProductId(Guid productId)
        {
            ProductId = productId;
        }
        public void SetCouponId(Guid couponId)
        {
            CouponId = couponId;
        }
    }
}
