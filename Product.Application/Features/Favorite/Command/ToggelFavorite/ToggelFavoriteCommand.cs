namespace Product.Application.Features.Favorite.Command.ToggelFavorit
{
    public class ToggelFavoriteCommand:ICommand
    {
        public Guid ProductId { get;  set; }
    }
}
