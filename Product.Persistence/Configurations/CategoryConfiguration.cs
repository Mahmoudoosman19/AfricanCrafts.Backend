using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(TableNames.Categories);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.NameAr).HasMaxLength(100);
            builder.Property(c => c.NameEn).HasMaxLength(100);
            builder.Property(c => c.ImageName).HasMaxLength(255);
            builder.Property(c => c.ImageFileId);

            builder.HasOne(c => c.ParentCategory)
                   .WithMany(c => c.SubCategories)
                   .HasForeignKey(c => c.ParentId);

            builder.HasOne(c => c.SizeGroup)
                   .WithMany()
                   .HasForeignKey(c => c.SizeGroupId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(s => s.NameAr)
                   .IsUnique();
            builder.HasIndex(s => s.NameEn)
                   .IsUnique();
        }
    }
}
