namespace Product.Application.Features.Product.Queries.GetProductByVendorId
{
    public class GetProductQueryResponse
    {
        public Guid ProductId { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductNameEn{ get; set; }
        public ICollection<string> Images { get; set; }
        public List<Domain.Entities.Review> Review { get; set; }
        public decimal ProductPrice {  get; set; }
        public decimal ProductRate {  get; set; }
        public string? ProductCode { get; set; }
    }
}
