namespace Product.Application.Features.SizeGroups.Queries.GetSizeGroupById;

public sealed class QuestionResponse
{
    public Guid Id { get; set; }
    public string? QuestionAr { get; private set; } = null!;
    public string? QuestionEn { get; private set; } = null!;
    public bool IsDeleted { get; set; }

}