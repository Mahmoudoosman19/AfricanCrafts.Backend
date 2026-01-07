using Product.Application.Features.Product.Queries.GetCommentsOnRejectedProducts;

namespace Product.Application.Specifications.Products
{
    public class GetProductByProductIdAndCreateDetaSpecification : Specification<Domain.Entities.ProductComment>
    {
        public GetProductByProductIdAndCreateDetaSpecification(GetCommentsOnRejectedProductsByProductIdQuery query)
        {

            AddCriteria(x => x.ProductId == query.ProductId);
            AddOrderByDescending(x => x.CreatedOnUtc);
        }
    }
}
