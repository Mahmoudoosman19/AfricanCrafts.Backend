using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Wallet.Queries.GetAdminWalletTransactions
{
    public class GetAdminWalletTransactionsQueryResponse
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid WalletId { get; set; }
        public decimal AmountOfTheOrder { get; set; }
        public decimal BalanceAfterTheTransaction { get; set; }
        public decimal BalanceBeforeTheTransaction { get; set; }
        public AdjustmentType adjustmentType { get; set; }
        public string VendorNameAr { get; set; }
        public string VendorNameEn { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
