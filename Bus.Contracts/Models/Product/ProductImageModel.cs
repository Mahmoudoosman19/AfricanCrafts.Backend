namespace Bus.Contracts.Models.Product;

public sealed record ProductImageModel
{
    public Guid Id { get; set; }
    public string ImageName { get; set; } = null!;
    public string ImageFolderName { get; set; } = null!;
    public string ColorCode { get; set; } = null!;
}
