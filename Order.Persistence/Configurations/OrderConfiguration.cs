using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Persistence.Constants;

namespace Order.Persistence.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.ToTable(TableNames.Orders);

            builder.HasKey(x => x.Id);

            builder.Property(p => p.CustomerId)
               .IsRequired();


            builder.Property(o => o.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.DiscountedPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(o => o.StatusId)
                   .IsRequired();

            builder.Property(o => o.PaymentStatusId)
                    .IsRequired();


           builder .Property(o => o.PaymentMethodId)
                     .IsRequired()
                     .HasDefaultValue(1);

            builder.HasMany(o => o.Items)
                   .WithOne()
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(o => o.Status)
                   .WithMany()
                   .HasForeignKey(o => o.StatusId)
                   .IsRequired();

            builder.HasOne(o => o.PaymentStatus)
                   .WithMany()
                   .HasForeignKey(o => o.PaymentStatusId)
                   .IsRequired();

            builder.HasOne(o => o.PaymentMethod)
                   .WithMany()
                   .HasForeignKey(o => o.PaymentMethodId)
                   .IsRequired();
            builder.Property(o => o.ReturnedReason)
        .IsRequired(false);

        }
    }
}
