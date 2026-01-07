namespace Product.Application.Features.Review.Queries.GetProductReviews
{
    public class GetProductReviewsQuery : IQuery<List<GetProductReviewsQueryResponse>>
    {
        public Guid ProductId { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
