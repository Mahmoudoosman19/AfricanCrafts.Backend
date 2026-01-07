using Product.Domain.Entities;

namespace Product.Application.Specifications.Products
{
    internal class GetProductExtensionsByProductIdSpecification : Specification<ProductExtension>
    {
        public GetProductExtensionsByProductIdSpecification(Guid productId)
        {
            AddCriteria(img => img.ProductId == productId);
        }
    }
}
