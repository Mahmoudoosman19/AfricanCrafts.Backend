namespace Product.Application.Features.Product.Queries.GetVendorProduct
{
    public class VendorGetProductsByVendorIdQuery : IQuery<GetVendorProductsResponse>
    {
        public Guid VendorId { get; set; }  
    }
}

