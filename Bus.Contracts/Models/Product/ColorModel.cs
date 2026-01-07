namespace Bus.Contracts.Models.Product;

public sealed record ColorModel
{
    public Guid Id { get; init; }
    public string NameAr { get; init; } = null!;
    public string NameEn { get; init; } = null!;
    public string Code { get; init; } = null!;
}
