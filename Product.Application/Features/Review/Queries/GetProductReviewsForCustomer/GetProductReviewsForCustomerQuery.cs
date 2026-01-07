namespace Product.Application.Features.Review.Queries.GetProductReviewsForCustomer
{
    public class GetProductReviewsForCustomerQuery : IQuery<GetProductReviewsForCustomerQueryResponse>
    {
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
