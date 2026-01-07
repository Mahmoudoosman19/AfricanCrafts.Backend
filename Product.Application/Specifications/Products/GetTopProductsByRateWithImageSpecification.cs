namespace Product.Application.Specifications.Products
{
    public class GetTopProductsByRateWithImageSpecification : Specification<Domain.Entities.Product>
    {
        public GetTopProductsByRateWithImageSpecification(int topCount)
        {
            AddOrderByDescending(x => x.Rate);
            ApplyPaging(topCount, 1);


            AddInclude(new List<string>
            {
                nameof(Domain.Entities.Product.Images),
            });
        }
    }
}