using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;
using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Entities
{
    public class WalletTransaction : Entity<Guid>, IAuditableEntity
    {
        public Guid OrderId { get; private set; }
        public decimal AmountOfTheOrder { get; private set; }
        public decimal BalanceAfterTheTransaction { get; private set; }
        public decimal BalanceBeforeTheTransaction { get; private set; }
        public AdjustmentType adjustmentType { get; private set; }
        public Guid WalletId { get; private set; }
        [ForeignKey(nameof(WalletId))]
        public virtual Wallet Wallet { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public void SetOrderId(Guid orderId)
        {
            OrderId = orderId;
        }
        public void SetPriceOfTheOrder(decimal amountOfTheOrder)
        {
            AmountOfTheOrder = amountOfTheOrder;
        }
        public void SetBalanceAfterTheTransaction(decimal balanceAfterTheTransaction)
        {
            BalanceAfterTheTransaction = balanceAfterTheTransaction;
        }
        public void SetBalanceBeforeTheTransaction(decimal balanceBeforeTheTransaction)
        {
            BalanceBeforeTheTransaction = balanceBeforeTheTransaction;
        }
        public void SetAdjustmentType(AdjustmentType adjustmentType)
        {
            this.adjustmentType = adjustmentType;
        }
        public void SetWalletId(Guid walletId)
        {
            WalletId = walletId;
        }

    }
}
