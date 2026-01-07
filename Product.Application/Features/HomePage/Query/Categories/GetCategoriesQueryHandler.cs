using Product.Application.Specifications.GenarceSpecificationToHomePage;
using Product.Domain.Entities;

namespace Product.Application.Features.HomePage.Query.Categories
{
    internal class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, IReadOnlyList<GetCategoriesQueryResponse>>
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IGenericRepository<Category> categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        public async Task<ResponseModel<IReadOnlyList<GetCategoriesQueryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            (var categories, int count) = _categoryRepo.GetWithSpec(new GetCategoryByStatusWithSizeGroupAndParentCategorySpecification(request));
            var mappingCategories = _mapper.Map<IReadOnlyList<GetCategoriesQueryResponse>>(categories!);

            return ResponseModel.Success(mappingCategories!, count);
        }
    }
}
