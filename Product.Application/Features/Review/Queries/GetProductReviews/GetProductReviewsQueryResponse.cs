namespace Product.Application.Features.Review.Queries.GetProductReviews
{
    public class GetProductReviewsQueryResponse
    {
        public string? UserName { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public double Rate { get; set; }
        public string? Comment { get; set; }
    }
}
