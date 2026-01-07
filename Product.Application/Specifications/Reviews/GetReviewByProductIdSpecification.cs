namespace Product.Application.Specifications.Reviews
{
    public class GetReviewByProductIdSpecification : Specification<Domain.Entities.Review>
    {
        public GetReviewByProductIdSpecification(Guid productId)
        {
            AddCriteria(x => x.ProductId == productId);
        }
    }
}
