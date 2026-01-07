namespace Product.Application.Features.Product.Queries.GetCommentsOnRejectedProducts
{
    public class GetCommentsOnRejectedProductsByProductIdQuery : IQuery<ProductsQueryResponse>
    {
        public Guid ProductId { get; set; }
    }
}
