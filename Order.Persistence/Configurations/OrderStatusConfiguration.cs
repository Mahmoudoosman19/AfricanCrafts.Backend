using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;
using Order.Persistence.Constants;

namespace Order.Persistence.Configurations
{
    internal class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable(TableNames.OrderStatus);

            builder.HasKey(p => p.Id);

            builder.Property(os => os.Id)
                   .ValueGeneratedNever()
                   .IsRequired();

            builder.Property(os => os.NameAr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(os => os.NameEn)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
