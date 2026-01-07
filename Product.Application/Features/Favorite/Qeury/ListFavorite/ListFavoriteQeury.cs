namespace Product.Application.Features.Favorite.Qeury.ListFavorite
{
    public class ListFavoriteQeury: IQuery<IReadOnlyList<ListFavoriteQueryResponse>>
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
    }
}
