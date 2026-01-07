namespace Product.Application.Features.SizeGroups.Queries.GetSizeGroupById
{
    public sealed record SizeGroupWithSizesResponse
    {
        public Guid Id { get; init; }
        public string? NameAr { get; init; }
        public string? NameEn { get; init; }
        public IReadOnlyCollection<SizeResponse> Sizes { get; set; }
        public IReadOnlyCollection<QuestionResponse> SizeGroupQuestions { get; set; }
    }
}
