using Product.Domain.Entities;

namespace Product.Application.Features.Categories.Queries.GetListCategoryLookup
{
    internal class GetListCategoryLookupQueryHandler : IQueryHandler<GetListCategoryLookupQuery, IReadOnlyList<GetCategorylookupRespone>>
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public GetListCategoryLookupQueryHandler(
            IGenericRepository<Category> categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;

        }
        public Task<ResponseModel<IReadOnlyList<GetCategorylookupRespone>>> Handle(GetListCategoryLookupQuery request, CancellationToken cancellationToken)
        {
            var categories = _categoryRepo.Get();
            var mappingCategories = _mapper.Map<IReadOnlyList<GetCategorylookupRespone>>(categories);
            return Task.FromResult(ResponseModel.Success(mappingCategories));

        }

    }
}
