namespace Product.Application.Features.Product.Queries.GetProductDetailsCustomer
{
    public class CustomerGetProductDetailsByIdQuery:IQuery<CustomerProductDetailsQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
