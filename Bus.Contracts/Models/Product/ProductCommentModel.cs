namespace Bus.Contracts.Models.Product;

public sealed record ProductCommentModel
{
    public Guid Id { get; set; }
    public string Comment { get; set; } = null!;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
