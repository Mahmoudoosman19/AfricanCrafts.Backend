using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Data.Configurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable(TableNames.Coupons);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code).HasMaxLength(20);
            builder.Property(c => c.UserCount);
            builder.Property(c => c.DiscountPercentage);
            builder.Property(c => c.IsActive);
            builder.Property(c => c.StartDate);
            builder.Property(c => c.ExpireDate);
            builder.Property(c => c.UserId);

               builder.HasIndex(s => s.Code)
                   .IsUnique();
        }
    }
}
