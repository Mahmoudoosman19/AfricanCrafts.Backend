namespace Product.Application.Features.Product.Queries.GetProductByVendorId
{
    public class GetProductByVendorIdWithImageQuery:IQuery<List<GetProductQueryResponse>>
    {
        public Guid VendorId {  get; set; } 
    }
}
