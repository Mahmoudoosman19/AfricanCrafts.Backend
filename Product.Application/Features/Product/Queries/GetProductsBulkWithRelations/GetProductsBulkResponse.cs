using Product.Application.Features.Product.Queries.GetProductDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Features.Product.Queries.GetProductsBulkWithRelations
{
    public class GetProductsBulkResponse
    {
        public List<ProductDetailsQueryResponse> Products {  get; set; }
    }
}
