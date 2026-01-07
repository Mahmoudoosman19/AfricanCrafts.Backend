namespace Product.Application.Features.HomePage.Query.Categories
{
    public class GetCategoriesQuery : IQuery<IReadOnlyList<GetCategoriesQueryResponse>>
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
    }
}
