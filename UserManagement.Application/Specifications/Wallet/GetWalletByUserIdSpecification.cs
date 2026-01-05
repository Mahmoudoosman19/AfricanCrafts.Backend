using Common.Domain.Specification;

namespace UserManagement.Application.Specifications.Wallet
{
    public class GetWalletByUserIdSpecification : Specification<Domain.Entities.Wallet>
    {
        public GetWalletByUserIdSpecification(Guid userId)
        {
            AddCriteria(x => x.UserId == userId);
        }
    }
}
