using IdentityHelper.Abstraction;
using Product.Application.Specifications.Favorite;
using Product.Domain.Abstraction;

namespace Product.Application.Features.Favorite.Qeury.ListFavorite
{
    public class ListFavoriteQueryHandler: IQueryHandler<ListFavoriteQeury, IReadOnlyList<ListFavoriteQueryResponse>>
    {
        private readonly IProductRepository<Domain.Entities.Favorite> _favRepo;
        private readonly IMapper _mapper;
        private readonly ITokenExtractor _tokenExtractor;


        public ListFavoriteQueryHandler(IMapper mapper, IProductRepository<Domain.Entities.Favorite> favRepo, ITokenExtractor tokenExtractor)
        {

            _mapper = mapper;
            _favRepo = favRepo;
            _tokenExtractor = tokenExtractor;
        }

        public async Task<ResponseModel<IReadOnlyList<ListFavoriteQueryResponse>>> Handle(ListFavoriteQeury request, CancellationToken cancellationToken)
        {
            var userId = _tokenExtractor.GetUserId();
            var (listQuery, count) = _favRepo.GetWithSpec(new GetFavoriteByCustomerIdWithProductImageAndProductSpecification(request,userId));
            var favorite = _mapper.Map <IReadOnlyList < ListFavoriteQueryResponse>>(listQuery);
            return ResponseModel.Success(favorite, count);

        }

  
    }
}
