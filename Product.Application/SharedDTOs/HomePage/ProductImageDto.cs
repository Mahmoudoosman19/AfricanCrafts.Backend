namespace Product.Application.SharedDTOs.HomePage
{
    public sealed record ProductImageDto
    {
        public string imageName { get; init; } = null!;
        public string colorCode { get; init; } = null!;
    }
}
