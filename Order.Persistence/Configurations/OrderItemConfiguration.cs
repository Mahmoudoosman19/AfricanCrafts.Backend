using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;
using Order.Persistence.Constants;

namespace Order.Persistence.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable(TableNames.OrderItems);

            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.ProductId)
                .IsRequired();

            builder.Property(oi => oi.OrderId)
                .IsRequired();

            builder.Property(oi => oi.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne<Domain.Entities.Order>()
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired();
        }
    }
}
