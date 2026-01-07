using Product.Application.SharedDTOs.SizeGroup;

namespace Product.Application.Features.SizeGroups.Queries.GetSizeGroups
{
    public sealed class GetSizeGroupsQuery : IQuery<IEnumerable<SizeGroupLookupDto>>
    {
        public int PageSize { get; init; } = 20;
        public int PageIndex { get; init; } = 1;
        public string? NameSearch { get; set; }
    }
}
