using Product.Application.Features.Product.Queries.GetProductDetails;
using Product.Domain.Entities;

namespace Product.Application.Specifications.Products
{
    internal class GetProductDetailsByIdWithRelationsProductSpecification : Specification<Domain.Entities.Product>
    {
        public GetProductDetailsByIdWithRelationsProductSpecification(GetProductDetailsByIdWithRelationsProductQuery query)
        {
            AddCriteria(x => x.Id == query.Id);

            AddInclude(new List<string>
            {
                nameof(Domain.Entities.Product.Category),
                nameof(Domain.Entities.Product.Points),
                nameof(Domain.Entities.Product.Images),
                nameof(Domain.Entities.Product.Extensions),
                nameof(Domain.Entities.Product.Comments),
                $"{nameof(Domain.Entities.Product.Extensions)}.{nameof(ProductExtension.Size)}",
            });

            EnableSplitQuery();
        }
    }
}
