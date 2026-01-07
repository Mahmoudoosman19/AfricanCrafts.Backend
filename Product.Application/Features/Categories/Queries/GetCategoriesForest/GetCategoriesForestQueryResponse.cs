using Product.Application.Features.Categories.Dto;

namespace Product.Application.Features.Categories.Queries.GetCategoriesForest;

public sealed record GetCategoriesForestQueryResponse
{
    public List<CategoryTreeDto> Categories { get; init; } = new List<CategoryTreeDto>();
}