namespace Product.Application.Specifications.Favorite
{
    public class GetFavoriteProductByCustomerIdSpecification : Specification<Domain.Entities.Favorite>
    {
        public GetFavoriteProductByCustomerIdSpecification(Guid customerId)
        {
            AddCriteria(x => x.CustomerId == customerId);

        }
    }
}
