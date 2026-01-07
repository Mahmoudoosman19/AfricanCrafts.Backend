namespace Product.Application.Features.Review.Commands.UpdateReview
{
    public class UpdateReviewCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public double Rate { get; set; }
        public string? Comment { get; set; }
    }
}
