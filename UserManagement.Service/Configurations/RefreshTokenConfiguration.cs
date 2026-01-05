using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Entities;
using UserManagement.Persistence.Constants;

namespace UserManagement.Persistence.Configurations;

internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable(TableNames.RefreshTokens);

        builder.HasKey(x => new { x.UserId, x.Token });


        builder.Property(rt => rt.Token)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(rt => rt.ExpiresOn)
               .IsRequired();

        builder.Property(rt => rt.RevokedOn)
               .IsRequired(false);

        builder.Property(rt => rt.CreatedOnUtc)
               .IsRequired();

        builder.Property(rt => rt.ModifiedOnUtc)
               .IsRequired(false);

        builder.HasOne(rt => rt.User)
               .WithMany()
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(rt => rt.Token).IsUnique();
    }
}
