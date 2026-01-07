using Product.Application.Features.Favorite.Qeury.ListFavorite;

namespace Product.Application.Specifications.Favorite
{
    internal class GetFavoriteByCustomerIdWithProductImageAndProductSpecification: Specification<Domain.Entities.Favorite>
    {
        public GetFavoriteByCustomerIdWithProductImageAndProductSpecification(ListFavoriteQeury request, Guid userId)
        {
            AddCriteria(x => x.CustomerId == userId);
            AddInclude(nameof(Domain.Entities.Favorite.Product));
            AddInclude("Product.Images");
            ApplyPaging(request.PageSize, request.PageIndex);

        }
    }
}
