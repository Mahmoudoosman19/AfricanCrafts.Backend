using Product.Application.Features.Product.Queries.GetVendorProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Features.Product.Queries.GetVendorProducts.GetVendorProductTopFive
{
    public class GetVendorProductTopFiveQuery : IQuery<IEnumerable<GetVendorProductTopFiveResponce>>
    {
        public Guid VendorId { get; set; }
    }
}
