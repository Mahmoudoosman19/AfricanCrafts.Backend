using Product.Application.SharedDTOs.SizeGroup;
using Product.Application.SharedDTOs.SizeGroupQuestion;

namespace Product.Application.Features.SizeGroups.Commands.CreateSizeGroup
{
    public sealed class CreateSizeGroupCommand : ICommand
    {
        public string? NameAr { get; init; }
        public string? NameEn { get; init; }
        public ICollection<CreateSizeDto> Sizes { get; set; }
        public ICollection<CreateSizeGroupQuestionDto> Questions { get; set; }
    }
}
