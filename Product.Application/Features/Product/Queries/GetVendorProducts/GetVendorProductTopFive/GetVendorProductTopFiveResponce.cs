namespace Product.Application.Features.Product.Queries.GetVendorProducts.GetVendorProductTopFive
{
    public class GetVendorProductTopFiveResponce
    {
        public Guid Id { get; set; } 
        public string NameAr { get;  set; } 
        public string NameEn { get;  set; }
        public string DescriptionAr { get;  set; } 
        public string DescriptionEn { get;  set; } 
        public decimal Price { get;  set; }
        public decimal DiscountPrice { get;  set; } 
    }
}
