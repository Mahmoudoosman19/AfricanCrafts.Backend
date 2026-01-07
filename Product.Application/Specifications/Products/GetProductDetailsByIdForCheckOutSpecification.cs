using Product.Application.Features.Product.Queries.GetProductDetailsForCheckOut;
using Product.Domain.Entities;
using System.Linq;

namespace Product.Application.Specifications.Products
{
    internal class GetProductDetailsByIdForCheckOutSpecification : Specification<Domain.Entities.Product>
    {
        public GetProductDetailsByIdForCheckOutSpecification(GetProductDetailsForCheckOutQuery query)
        {
            //AddCriteria(x => x.Id == query.Id);

            AddInclude(new List<string>
            {
                nameof(Domain.Entities.Product.Category),
                nameof(Domain.Entities.Product.Points),
                nameof(Domain.Entities.Product.Images),
                nameof(Domain.Entities.Product.Extensions),
                nameof(Domain.Entities.Product.Comments),
                $"{nameof(Domain.Entities.Product.Extensions)}.{nameof(ProductExtension.Size)}",
            });
            AddCriteria(x => x.Id == query.Id && x.Extensions.Any(e => e.Id == query.extensionsId));
            EnableSplitQuery();
        }
    }
}
