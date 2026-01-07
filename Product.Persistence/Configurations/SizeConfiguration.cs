using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Data.Configurations
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.ToTable(TableNames.Sizes);

            builder.HasKey(s => s.Id);

            builder.Property(s => s.NameAr).HasMaxLength(100);
            builder.Property(s => s.NameEn).HasMaxLength(100);
            builder.Property(s => s.DescriptionAr).HasMaxLength(512);
            builder.Property(s => s.DescriptionEn).HasMaxLength(512);

            builder.HasOne(s => s.SizeGroup)
                   .WithMany(s => s.Sizes)
                   .HasForeignKey(s => s.SizeGroupId)
                   .OnDelete(DeleteBehavior.Cascade);

                    builder.HasIndex(s => new { s.NameAr, s.SizeGroupId })
                           .IsUnique();

                    builder.HasIndex(s => new { s.NameEn, s.SizeGroupId })
                           .IsUnique();
        }
    }
}
