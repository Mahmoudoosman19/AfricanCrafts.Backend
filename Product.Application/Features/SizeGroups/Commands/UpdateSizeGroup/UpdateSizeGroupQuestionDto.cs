using Common.Application.Extensions.Mapster;

namespace Product.Application.Features.SizeGroups.Commands.UpdateSizeGroup;

public class UpdateSizeGroupQuestionDto : UpdateNestedListDto<Guid>
{
    public string? QuestionAr { get; set; }
    public string? QuestionEn { get; set; }
    public bool IsDeleted { get; set; }
}