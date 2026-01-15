using Product.Application.Features.Product.Queries.GetProductsBulkWithRelations;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Specifications.Products
{
    public class GetProductsBulkWithRelationsSpecification : Specification<Domain.Entities.Product>
    {
        public GetProductsBulkWithRelationsSpecification(GetProductsBulkWithRelationsQuery request)
        {
            AddCriteria(p => request.Ids.Contains(p.Id));
            AddInclude(new List<string>
            {
                nameof(Domain.Entities.Product.Category),
                nameof(Domain.Entities.Product.Images),
                nameof(Domain.Entities.Product.Extensions),
                $"{nameof(Domain.Entities.Product.Extensions)}.{nameof(ProductExtension.Size)}",
            });
        }
    }
}
