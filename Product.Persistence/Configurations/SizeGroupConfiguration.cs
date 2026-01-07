using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Data.Configurations
{
    public class SizeGroupConfiguration : IEntityTypeConfiguration<SizeGroup>
    {
        public void Configure(EntityTypeBuilder<SizeGroup> builder)
        {
            builder.ToTable(TableNames.SizeGroups);

            builder.HasKey(sg => sg.Id);

            builder.Property(sg => sg.NameAr).HasMaxLength(100);
            builder.Property(sg => sg.NameEn).HasMaxLength(100);

            builder.HasIndex(s => s.NameAr)
                   .IsUnique();
            builder.HasIndex(s => s.NameEn)
                   .IsUnique();
        }
    }
}
