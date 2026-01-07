using Product.Application.Features.Product.Queries.GetProducts;
using Product.Domain.Enums;

namespace Product.Application.Specifications.Products
{
    internal sealed class GetProductsByStatusAndVendorIdAndProductCodeAndNameWithImageSpecification : Specification<Domain.Entities.Product>
    {
        public GetProductsByStatusAndVendorIdAndProductCodeAndNameWithImageSpecification(GetProductsByStatusAndVendorIdAndProductCodeAndNameWithImageQuery query,
            Guid userId, string role)
        {
            ApplyPaging(query.PageSize, query.PageIndex);

            if(query.IsActive.HasValue)
                AddCriteria(x => x.IsActive == query.IsActive);

            if(!string.IsNullOrEmpty(query.Name))
                AddCriteria(x => x.NameAr.Contains(query.Name) || x.NameEn.Contains(query.Name));

            if (!string.IsNullOrEmpty(query.ProductCode))
                AddCriteria(x => x.ProductCode.Contains(query.ProductCode));

            if (query.Status!=null)
                AddCriteria(x => x.Status==(ProductStatus)query.Status);

            if (role == "Vendor")
                AddCriteria(x => x.VendorId == userId);

            AddInclude(new List<string>
            {
                nameof(Domain.Entities.Product.Images),
            });
        }
        public GetProductsByStatusAndVendorIdAndProductCodeAndNameWithImageSpecification(string ProductCode)
        {
            AddCriteria(x => x.ProductCode == ProductCode);
        }
    }
}
