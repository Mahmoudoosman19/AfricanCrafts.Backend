namespace Product.Application.Specifications.Favorite
{
    public class ToggelFavoriteSpecification: Specification<Domain.Entities.Favorite>   

    {

        public ToggelFavoriteSpecification(Guid ProductId,Guid CusrtomerId)
        {
                AddCriteria(c => c.ProductId == ProductId&&c.CustomerId==CusrtomerId);

        }
    }
}
