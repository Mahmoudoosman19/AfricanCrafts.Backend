namespace Bus.Contracts.Models.Product;

public sealed record PointModel
{
    public Guid Id { get; set; }
    public string NameAr { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public decimal Money { get; set; }
    public int RewardedPoints { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
