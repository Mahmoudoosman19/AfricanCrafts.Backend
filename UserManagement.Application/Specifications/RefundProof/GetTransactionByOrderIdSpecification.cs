using Common.Domain.Specification;


namespace UserManagement.Application.Specifications.RefundProof
{
    public class GetTransactionByOrderIdSpecification : Specification<Domain.Entities.WalletTransaction>
    {
        public GetTransactionByOrderIdSpecification(Guid orderId)
        {
            AddCriteria(x => x.OrderId == orderId);
        }
    }
}
