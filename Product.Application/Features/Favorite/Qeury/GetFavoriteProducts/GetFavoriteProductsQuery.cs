namespace Product.Application.Features.Favorite.Qeury.GetFavoriteProducts
{
    public class GetFavoriteProductsQuery : IQuery<List<Guid>>
    {
        public Guid CustomerId { get; set; }
    }
}
