using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;
using Order.Persistence.Constants;

namespace Order.Persistence.Configurations
{
    internal class PaymentStatusConfiguration : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder)
        {
            builder.ToTable(TableNames.PaymentStatus);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .ValueGeneratedNever()
                   .IsRequired();

            builder.Property(p => p.NameAr)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.NameEn)
                   .IsRequired()
                   .HasMaxLength(100);
        }

    }
}
