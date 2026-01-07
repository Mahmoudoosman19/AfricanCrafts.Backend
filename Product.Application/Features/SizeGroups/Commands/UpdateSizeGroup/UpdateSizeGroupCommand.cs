namespace Product.Application.Features.SizeGroups.Commands.UpdateSizeGroup
{
    public sealed class UpdateSizeGroupCommand : ICommand
    {
        public Guid Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public List<UpdateSizeDto> EditedSizes { get; set; } = [];
        public List<UpdateSizeGroupQuestionDto> EditedQuestions { get; set; } = [];
    }
}
