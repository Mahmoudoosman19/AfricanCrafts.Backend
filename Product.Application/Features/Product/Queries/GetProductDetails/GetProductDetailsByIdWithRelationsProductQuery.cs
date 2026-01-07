namespace Product.Application.Features.Product.Queries.GetProductDetails
{
	public class GetProductDetailsByIdWithRelationsProductQuery : IQuery<ProductDetailsQueryResponse>
	{
		public Guid Id { get; set; }
	}
}