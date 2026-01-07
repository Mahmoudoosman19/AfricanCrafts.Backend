using Product.Application.Features.Product.Queries.GetVendorProduct;
using Product.Application.Features.Product.Queries.GetVendorProducts.GetVendorProductTopFive;

namespace Product.Application.Specifications.Products
{
    internal class VendorGetProductByVendorIdSpecification : Specification<Domain.Entities.Product>
    {
        public VendorGetProductByVendorIdSpecification(VendorGetProductsByVendorIdQuery query)
        {
            AddCriteria(x=>x.VendorId==query.VendorId);
            
        }
        public VendorGetProductByVendorIdSpecification(GetVendorProductTopFiveQuery query)
        {
            AddCriteria(x => x.VendorId == query.VendorId);
            AddOrderByDescending(x => x.Rate);
            ApplyPaging(5, 1);
        }
    }
}
