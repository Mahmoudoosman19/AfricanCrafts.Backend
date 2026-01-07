namespace Product.Application.Specifications.Products
{
    internal class GetProductByVendorIdWithImageSpecification : Specification<Domain.Entities.Product>
    {
        public GetProductByVendorIdWithImageSpecification(Guid vendorId)
        {
            AddCriteria(x => vendorId == x.VendorId);
            AddInclude(new List<string> { nameof(Product.Domain.Entities.Product.Images) });
        }
    }
}
