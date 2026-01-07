using Product.Application.Specifications.Favorite;

namespace Product.Application.Features.Favorite.Qeury.GetFavoriteProducts
{
    internal class GetFavoriteProductsQueryHandler : IQueryHandler<GetFavoriteProductsQuery, List<Guid>>
    {
        private readonly IGenericRepository<Domain.Entities.Favorite> _favoriteRepo;
        private readonly IMapper _mapper;
        public GetFavoriteProductsQueryHandler(IGenericRepository<Domain.Entities.Favorite> favoriteRepo, IMapper mapper)
        {
            _favoriteRepo = favoriteRepo;
            _mapper = mapper;
        }
        public async Task<ResponseModel<List<Guid>>> Handle(GetFavoriteProductsQuery request, CancellationToken cancellationToken)
        {
            var favoriteProducts = _favoriteRepo.GetWithSpec(new GetFavoriteProductByCustomerIdSpecification(request.CustomerId)).data
                                               .Select(p => p.ProductId).ToList();
            return ResponseModel.Success(favoriteProducts);
        }
    }
}
