using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Persistence.Configurations
{
    internal sealed class ProductExtensionConfiguration : IEntityTypeConfiguration<ProductExtension>
    {
        public void Configure(EntityTypeBuilder<ProductExtension> builder)
        {
            builder.ToTable(TableNames.ProductExtensions);

            builder.HasKey(e => e.Id);

            builder.Property(pe => pe.Fees).HasColumnType("decimal(18,2)");

            builder.HasOne(pe => pe.Product)
                   .WithMany(p => p.Extensions)
                   .HasForeignKey(pe => pe.ProductId);

            builder.HasOne(pe => pe.Size)
                   .WithMany()
                   .HasForeignKey(pe => pe.SizeId)
                   .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
