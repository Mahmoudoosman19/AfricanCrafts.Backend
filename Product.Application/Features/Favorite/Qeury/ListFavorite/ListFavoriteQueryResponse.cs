namespace Product.Application.Features.Favorite.Qeury.ListFavorite
{
    public class ListFavoriteQueryResponse
    {
        public Guid ProductId { get; set; }
        public string productName { get; set; }
        public string Images { get; set; }
        public decimal Price { get;  set; }
        public decimal DiscountPrice { get;  set; } = 0;

    }
}
