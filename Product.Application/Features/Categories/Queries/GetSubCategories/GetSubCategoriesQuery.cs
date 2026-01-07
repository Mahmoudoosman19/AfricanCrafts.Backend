using Product.Application.Features.Categories.Queries.GetParentCategories;

namespace Product.Application.Features.Categories.Queries.GetSubCategories;

public record GetSubCategoriesQuery : GetParentCategoriesQuery, IQuery<CategoryResponse>
{
    public Guid Id { get; set; }

}
