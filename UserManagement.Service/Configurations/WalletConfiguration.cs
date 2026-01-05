using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UserManagement.Persistence.Constants;

namespace UserManagement.Persistence.Configurations
{
    internal class WalletConfiguration : IEntityTypeConfiguration<Domain.Entities.Wallet>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Wallet> builder)
        {
            builder.ToTable(TableNames.Wallet);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.NumberOfCompletedTransactions)
             .IsRequired();

            builder.Property(o => o.CurrentBalance)
               .IsRequired()
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0);

            builder.Property(o => o.TotalEarnings)
               .IsRequired()
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0);

            builder.HasMany(o => o.walletTransaction)
                   .WithOne()
                   .HasForeignKey(oi => oi.WalletId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired(); 
            
            builder.HasOne(builder => builder.User)
                   .WithOne()
                   .HasForeignKey<Domain.Entities.Wallet>(o => o.UserId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();   



        }
    }
}
