namespace Product.Application.Specifications.Reviews
{
    public class GetReviewByUserIdAndProductReviewIdSpecification : Specification<Domain.Entities.Review>
    {
        public GetReviewByUserIdAndProductReviewIdSpecification(Guid Id, Guid userId)
        {
            AddCriteria(x => x.Id == Id && x.UserId == userId);
        }
    }
}
