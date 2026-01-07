using Product.Application.Features.Categories.Queries.GetCategroyById;

namespace Product.Application.Features.Categories.Queries.GetListCategory
{
    public class GetCategoryByStatusWithFilterSearchQuery : IQuery<IReadOnlyList<GetCategoryDetailsResponse>>
    {
        public string? Search { get; set; }
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        public bool? IsActive { get; set; }
    }
}
