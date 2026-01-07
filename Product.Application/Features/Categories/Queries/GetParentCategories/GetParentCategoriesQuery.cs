namespace Product.Application.Features.Categories.Queries.GetParentCategories;

public record GetParentCategoriesQuery : IQuery<IEnumerable<CategoryResponse>>
{
    public string? Name { get; set; }
    public int PageSize { get; set; } = 20;
    public int PageIndex { get; set; } = 1;
    public bool? IsActive { get; set; }
}
