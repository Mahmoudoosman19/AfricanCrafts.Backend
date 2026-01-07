using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Data.Configurations
{
    public class PointConfiguration : IEntityTypeConfiguration<Point>
    {
        public void Configure(EntityTypeBuilder<Point> builder)
        {
            builder.ToTable(TableNames.Points);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.NameAr).HasMaxLength(100);
            builder.Property(p => p.NameEn).HasMaxLength(100);
            builder.Property(p => p.Money).HasColumnType("decimal(18,2)");
            builder.Property(p => p.RewardedPoints);

            builder.HasIndex(s => s.NameAr)
                   .IsUnique();
            builder.HasIndex(s => s.NameEn)
                   .IsUnique();
        }
    }
}
