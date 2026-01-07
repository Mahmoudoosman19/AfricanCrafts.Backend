using Product.Domain.Entities;

namespace Product.Application.Specifications.Products
{
    internal class GetProductImagesByProductIdSpecification : Specification<ProductImage>
    {
        public GetProductImagesByProductIdSpecification(Guid productId)
        {
            AddCriteria(img => img.ProductId == productId);
        }
    }
}
