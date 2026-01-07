namespace Product.Application.Features.Product.Queries.GetCommentsOnRejectedProducts
{
    public class ProductsQueryResponse
    {
        public string VendorNameAr { get; set; }
        public string VendorNameEn { get; set; }
        public string SupervisorNameAr { get; set; }
        public string SupervisorNameEn { get; set; }
        public string productNameAr { get; set; }
        public string productNameEn { get; set; }
        public List<CommentProductQueryResponse> Comments { get; set; } = new();
        public DateTime CreatedOnUtc { get; set; }
    }

    public class CommentProductQueryResponse
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
