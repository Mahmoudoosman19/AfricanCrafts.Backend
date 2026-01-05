using Common.Domain.Specification;
using UserManagement.Application.Features.wallet.Queries.GetWalletTransactions;

namespace UserManagement.Application.Specifications.Wallet
{
    internal class GetWalletTransactionsByIdSpecification : Specification<WalletTransaction>
    {
        public GetWalletTransactionsByIdSpecification(WalletTransactionQuery request)
        {
              if (request.UserId.HasValue)
                AddCriteria(x => x.Wallet.UserId == request.UserId);

            else if (request.UserWalletId.HasValue)
                AddCriteria(x => x.WalletId == request.UserWalletId);

            if (request.FromDate.HasValue && request.ToDate.HasValue)
                AddCriteria(x => x.CreatedOnUtc >= request.FromDate && x.CreatedOnUtc <= request.ToDate);

            AddInclude($"{nameof(Domain.Entities.WalletTransaction.Wallet)}");

            ApplyPaging(request.PageSize, request.PageIndex);
        }
    }
}
