namespace Product.Application.Features.Product.Queries.ProductStatisticsQuery
{
    public class TopProductDto
    {
        public Guid? ProductId { get; set; }
        public string NameAr { get;  set; } = null!;
        public string NameEn { get;  set; } = null!;
        public string? ProductCode { get; set; }
        public double Rate { get; set; } 
    }
}
