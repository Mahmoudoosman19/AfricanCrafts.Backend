using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Persistence.Configurations
{
    internal class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.ToTable(TableNames.Sliders);

            builder.HasKey(s => s.Id);

            builder.Property(s => s.NameAr)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(s => s.NameEn)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(s => s.ImageName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(s => s.ImageFileId)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(s => s.IsActive)
                .IsRequired();

            builder.Property(s => s.CreatedOnUtc)
                .IsRequired();

            builder.Property(s => s.ModifiedOnUtc);

            builder.HasOne(s => s.Category)
                .WithMany()
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(s => s.NameAr)
                   .IsUnique();
            builder.HasIndex(s => s.NameEn)
                   .IsUnique();
        }
    }
}
