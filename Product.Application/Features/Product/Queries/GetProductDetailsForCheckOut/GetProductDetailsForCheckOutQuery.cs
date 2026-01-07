namespace Product.Application.Features.Product.Queries.GetProductDetailsForCheckOut
{
    public class GetProductDetailsForCheckOutQuery : IQuery<GetProductDetailsForCheckOutQueryResponse>
    {
        public Guid Id { get; set; }
        public Guid extensionsId { get; set; }
        public int Quantity { get; set; }
    }
}
