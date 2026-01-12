using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;
using Order.Persistence.Constants;

namespace Order.Persistence.Configurations
{
    internal class PaymentMethodConfiguration: IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable(TableNames.PaymentMethods);

            builder.HasKey(pm => pm.Id);

            builder.Property(pm => pm.Id)
                   .ValueGeneratedNever()
                   .IsRequired();

            builder.Property(pm => pm.NameAr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(pm => pm.NameEn)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
