namespace UserManagement.Application.Features.wallet.Queries.GetWallet
{
    public class GetWalletResponse
    {
        public decimal CurrentBalance { get; set; }
        public long NumberOfCompletedTransactions { get; set; }
        public decimal TotalEarnings { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

    }
}
