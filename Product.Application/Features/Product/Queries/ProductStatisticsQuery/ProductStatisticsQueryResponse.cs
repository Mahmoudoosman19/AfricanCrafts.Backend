namespace Product.Application.Features.Product.Queries.ProductStatisticsQuery
{
    public class ProductStatisticsQueryResponse
    {
        public int TotalProductsCount { get; set; } 
        public List<TopProductDto> TopProducts { get; set; } 
    }
}
