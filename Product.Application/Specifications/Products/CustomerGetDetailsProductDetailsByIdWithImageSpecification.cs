using Product.Application.Features.Product.Queries.GetProductDetailsCustomer;

namespace Product.Application.Specifications.Products
{
    internal class CustomerGetDetailsProductDetailsByIdWithImageSpecification: Specification<Domain.Entities.Product>
    {
        public CustomerGetDetailsProductDetailsByIdWithImageSpecification( CustomerGetProductDetailsByIdQuery query)
        {
            AddCriteria(x => x.Id == query.Id);

            AddInclude(new List<string>
            {
                nameof(Domain.Entities.Product.Images)
            });

            EnableSplitQuery();
        }
    }
}
