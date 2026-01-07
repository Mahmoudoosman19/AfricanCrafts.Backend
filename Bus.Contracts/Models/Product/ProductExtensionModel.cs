namespace Bus.Contracts.Models.Product;

public sealed record ProductExtensionModel
{
    public Guid Id { get; set; }
    public string ColorCode { get; set; } = null!;
    public SizeModel Size { get; set; }
    public int Amount { get; set; }
    public decimal Fees { get; set; }
    public Guid ProductId { get; set; }
}
