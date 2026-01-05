using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.Wallet.Queries.GetAdminWalletTransactions
{
    public class GetAdminWalletTransactionsQuery : IQuery<IEnumerable<GetAdminWalletTransactionsQueryResponse>>
    {
        public string? VendorName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
    }
}
