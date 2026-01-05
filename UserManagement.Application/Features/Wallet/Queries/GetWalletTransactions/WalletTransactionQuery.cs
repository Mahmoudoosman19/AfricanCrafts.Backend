using Common.Application.Abstractions.Messaging;


namespace UserManagement.Application.Features.wallet.Queries.GetWalletTransactions
{
    public class WalletTransactionQuery : IQuery<List<WalletTransactionQueryResponse>>
    {
        public Guid? UserId { get; set; }
        public Guid ? UserWalletId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;

    }
}
