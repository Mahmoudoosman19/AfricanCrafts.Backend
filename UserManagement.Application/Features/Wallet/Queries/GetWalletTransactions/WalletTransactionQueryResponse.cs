using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.wallet.Queries.GetWalletTransactions
{
    public class WalletTransactionQueryResponse
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid WalletId { get; set; }
        public decimal AmountOfTheOrder { get; set; }
        public decimal BalanceAfterTheTransaction { get; set; }
        public decimal BalanceBeforeTheTransaction { get; set; }
        public AdjustmentType adjustmentType { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

    }
}
