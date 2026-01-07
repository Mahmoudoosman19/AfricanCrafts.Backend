namespace Product.Application.Specifications.Reviews
{
    public class GetReviewByUserIdAndProductIdSpecification : Specification<Domain.Entities.Review>
    {
        public GetReviewByUserIdAndProductIdSpecification(Guid productId, Guid userId)
        {
            AddCriteria(x => x.ProductId == productId && x.UserId == userId);
        }
    }
}
