namespace Product.Application.Features.SizeGroups.Queries.GetSizeGroupById
{
    public sealed class SizeResponse
    {
        public Guid Id { get; init; }
        public string? NameAr { get; init; }
        public string? NameEn { get; init; }
        public string? DescriptionAr { get; init; }
        public string? DescriptionEn { get; init; }
        public bool IsActive { get; set; }
    }
}
