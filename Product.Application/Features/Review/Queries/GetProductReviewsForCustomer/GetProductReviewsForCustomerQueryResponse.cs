namespace Product.Application.Features.Review.Queries.GetProductReviewsForCustomer
{
    public class GetProductReviewsForCustomerQueryResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public double Rate { get; set; }
        public string? Comment { get; set; }
    }
}
