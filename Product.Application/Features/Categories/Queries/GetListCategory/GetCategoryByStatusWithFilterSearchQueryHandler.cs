using Product.Application.Features.Categories.Queries.GetCategroyById;
using Product.Application.Specifications.Categories;
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Categories.Queries.GetListCategory
{
    internal class GetCategoryByStatusWithFilterSearchQueryHandler : IQueryHandler<GetCategoryByStatusWithFilterSearchQuery, IReadOnlyList<GetCategoryDetailsResponse>>
    {
        private readonly IProductRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public GetCategoryByStatusWithFilterSearchQueryHandler(
            IProductRepository<Category> categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;

        }
        public Task<ResponseModel<IReadOnlyList<GetCategoryDetailsResponse>>> Handle(GetCategoryByStatusWithFilterSearchQuery request, CancellationToken cancellationToken)
        {
            (var categories, int count) = _categoryRepo.GetWithSpec(new GetCategoryByStatusWithFilterSearchWithSizeGroupAndParentCategorySpecification(request));
            var mappingCategories = _mapper.Map<IReadOnlyList<GetCategoryDetailsResponse>>(categories);
            return Task.FromResult(ResponseModel.Success(mappingCategories, count));

        }


    }
}
