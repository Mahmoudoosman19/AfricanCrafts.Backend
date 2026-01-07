namespace Product.Application.Features.SizeGroups.Commands.AddSizeToGroup
{
    public sealed class AddSizeToGroupCommand : ICommand
    {
        public Guid SizeGroupId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string? DescriptionAr { get; set; }
        public string? DescriptionEn { get; set; }
    }
}
