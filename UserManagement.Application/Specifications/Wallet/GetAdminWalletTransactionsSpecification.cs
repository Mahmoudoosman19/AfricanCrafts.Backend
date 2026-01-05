using Common.Domain.Specification;
using UserManagement.Application.Features.Wallet.Queries.GetAdminWalletTransactions;

namespace UserManagement.Application.Specifications.Wallet
{
    public class GetAdminWalletTransactionsSpecification : Specification<WalletTransaction>
    {

        public GetAdminWalletTransactionsSpecification(GetAdminWalletTransactionsQuery request)
        {
            if (!string.IsNullOrWhiteSpace(request.VendorName))
            {
                var vendorNameLower = request.VendorName.ToLower();

                AddCriteria(x =>
                    x.Wallet.User.FullNameAr.ToLower().Contains(vendorNameLower) ||
                    x.Wallet.User.FullNameEn.ToLower().Contains(vendorNameLower));
            }


            if (request.FromDate.HasValue && request.ToDate.HasValue)
            {
                DateTime fromDate = request.FromDate.Value.Date;
                DateTime toDate = request.ToDate.Value.Date.AddDays(1).AddTicks(-1);

                AddCriteria(x => x.CreatedOnUtc >= fromDate && x.CreatedOnUtc <= toDate);
            }

            AddInclude($"{nameof(Domain.Entities.WalletTransaction.Wallet)}");
            AddInclude($"{nameof(WalletTransaction.Wallet)}.{nameof(Domain.Entities.Wallet.User)}");

            if (request.PageSize > 0 && request.PageIndex >= 0)
                ApplyPaging(request.PageSize, request.PageIndex);
        }
    }
}
