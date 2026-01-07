using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Data.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable(TableNames.Colors);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.NameAr).HasMaxLength(100);
            builder.Property(c => c.NameEn).HasMaxLength(100);
            builder.Property(c => c.NameEn).HasMaxLength(10);
            builder.HasIndex(s => s.NameAr)
                    .IsUnique();
            builder.HasIndex(s => s.NameEn)
                    .IsUnique();
        }
    }
}
