namespace Product.Application.Features.Review.Commands.AddReview
{
    public class AddReviewCommand : ICommand
    {
        public Guid ProductId { get; init; }
        public double Rate { get; init; }
        public string? Comment { get; init; }
    }
}
