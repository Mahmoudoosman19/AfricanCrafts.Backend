using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Entities;
using UserManagement.Persistence.Constants;

namespace UserManagement.Persistence.Configurations;

internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.NameAr)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.NameEn)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.CreatedOnUtc)
            .IsRequired();

        builder.Property(p => p.ModifiedOnUtc)
            .IsRequired(false);

        builder.HasIndex(p => p.NameAr).IsUnique();
        builder.HasIndex(p => p.NameEn).IsUnique();
    }
}
