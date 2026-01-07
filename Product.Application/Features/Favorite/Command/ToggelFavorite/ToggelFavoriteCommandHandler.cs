using IdentityHelper.Abstraction;
using Product.Application.Features.Favorite.Command.ToggelFavorit;
using Product.Application.Specifications.Favorite;

namespace Product.Application.Features.Favorite.Command.ToggelFavorite
{
    public class ToggelFavoriteCommandHandler : ICommandHandler<ToggelFavoriteCommand>
    {
       
        private readonly IGenericRepository<Domain.Entities.Favorite> _favoriteRepo;
        private readonly ITokenExtractor _tokenExtractor;
        public ToggelFavoriteCommandHandler(
            IGenericRepository<Domain.Entities.Favorite> favoriteRepo,
            ITokenExtractor tokenExtractor)
        {
           
            _favoriteRepo = favoriteRepo;
           
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel> Handle(ToggelFavoriteCommand request, CancellationToken cancellationToken)
        {
         
            var userId=_tokenExtractor.GetUserId(); 
            var userfavorite= _favoriteRepo.GetEntityWithSpec(new ToggelFavoriteSpecification(request.ProductId, userId));
            if (userfavorite == null)
            {
                var newFav = new Domain.Entities.Favorite(request.ProductId, userId);
                await _favoriteRepo.AddAsync(newFav);
            }   
            else
            {
                _favoriteRepo.Delete(userfavorite);
            }
            await _favoriteRepo.SaveChangesAsync(cancellationToken);
            return ResponseModel.Success();
        }
    }
}

