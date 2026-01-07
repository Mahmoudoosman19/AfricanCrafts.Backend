using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Data.Configurations
{
    public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.ToTable(TableNames.Advertisements);

            builder.HasKey(a => a.Id);

            builder.Property(a => a.NameAr).HasMaxLength(100);
            builder.Property(a => a.NameEn).HasMaxLength(100);
            builder.Property(a => a.ImageName).HasMaxLength(255);
            builder.Property(a => a.ImageFileId);
            builder.Property(a => a.DescriptionAr).HasMaxLength(255);
            builder.Property(a => a.DescriptionEn).HasMaxLength(255);

            builder.HasIndex(s => s.NameAr)
                   .IsUnique();
            builder.HasIndex(s => s.NameEn)
                   .IsUnique();
        }
    }
}
