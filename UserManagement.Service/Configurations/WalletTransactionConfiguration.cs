using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Entities;
using UserManagement.Persistence.Constants;

namespace UserManagement.Persistence.Configurations
{
    internal class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
    {
        public void Configure(EntityTypeBuilder<WalletTransaction> builder)
        {
            builder.ToTable(TableNames.WalletTransaction);

            builder.HasKey(wt => wt.Id);

            builder.Property(wt => wt.OrderId)
                .IsRequired();

            builder.Property(wt => wt.AmountOfTheOrder)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(wt => wt.BalanceAfterTheTransaction)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(wt => wt.BalanceBeforeTheTransaction)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(wt => wt.adjustmentType)
                .IsRequired();

            builder.Property(wt => wt.WalletId)
                .IsRequired();

            builder.HasOne(wt => wt.Wallet)
                .WithMany(vw => vw.walletTransaction)
                .HasForeignKey(wt => wt.WalletId);
        }
    }
}
