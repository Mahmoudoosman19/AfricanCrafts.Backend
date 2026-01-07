using Product.Domain.Entities;

namespace Product.Application.Specifications.Brova
{
    internal class GetBrovaItemsByProductIdWithProductAndProductImageSpecification : Specification<ProductExtension>
    {
        public GetBrovaItemsByProductIdWithProductAndProductImageSpecification(List<Guid> productsIds)
        {
            AddCriteria(x => productsIds.Contains(x.ProductId) && x.IsDeleted == false);
            AddInclude("Product");
            AddInclude("Product.Images");
        }
    }
}
