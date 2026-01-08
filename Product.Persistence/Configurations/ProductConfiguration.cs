using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Persistence.Constants;

namespace Product.Persistence.Configurations
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Domain.Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Product> builder)
        {
            builder.ToTable(TableNames.Products);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.NameAr).HasMaxLength(100);
            builder.Property(p => p.NameEn).HasMaxLength(100);
            builder.Property(p => p.DescriptionAr).HasMaxLength(512);
            builder.Property(p => p.DescriptionEn).HasMaxLength(512);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.DiscountPrice).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Rate).HasDefaultValue(2.5);

            builder.HasOne(p => p.Category)
                   .WithMany()
                   .HasForeignKey(p => p.CategoryId);


         


        }
    }
}
