using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Persistence.Configurations
{
    internal class productCouponsConfiguration : IEntityTypeConfiguration<Domain.Entities.CouponProducts>
    {
        public void Configure(EntityTypeBuilder<CouponProducts> builder)
        {
            builder.ToTable(TableNames.CouponProducts);
            builder.HasKey(x => x.Id);

            builder.HasOne(c => c.Coupon)
           .WithMany(p => p.Items)
           .HasForeignKey(pe => pe.CouponId)
           .IsRequired();
        }
    }
}
