namespace Product.Application.Features.Product.Queries.GetProductDetails
{
    public sealed record PointResponse
    {
        public Guid Id { get; set; }
        public string NameAr { get; private set; } = null!;
        public string NameEn { get; private set; } = null!;
        public decimal Money { get; private set; }
        public int RewardedPoints { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
