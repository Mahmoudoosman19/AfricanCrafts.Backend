using Common.Application.Extensions.Mapster;

namespace Product.Application.Features.SizeGroups.Commands.UpdateSizeGroup
{
    public class UpdateSizeDto : UpdateNestedListDto<Guid>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string? DescriptionAr { get; set; }
        public string? DescriptionEn { get; set; }
    }
}
