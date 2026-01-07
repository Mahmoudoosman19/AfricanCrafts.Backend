using Product.Application.Features.Categories.Queries.GetParentCategories;
using Product.Application.Specifications.Categories;
using Product.Domain.Entities;

namespace Product.Application.Features.Categories.Queries.GetSubCategories;

internal class GetSubCategoriesQueryHandler :
    IQueryHandler<GetSubCategoriesQuery,
        IEnumerable<CategoryResponse>>
{
    private readonly IGenericRepository<Category> _categoryRepo;
    private readonly IMapper _mapper;

    public GetSubCategoriesQueryHandler(IGenericRepository<Category> categoryRepo
        , IMapper mapper)
    {
        _categoryRepo = categoryRepo;
        _mapper = mapper;
    }
    public Task<ResponseModel<IEnumerable<CategoryResponse>>> Handle(GetSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = _categoryRepo.GetWithSpec(new GetSubCategoriesSpecification(request));
        if (categories.data == null)
            return Task.FromResult(ResponseModel.Success<IEnumerable<CategoryResponse>>([], Messages.NoCategoriesAreFound));


        return Task.FromResult(ResponseModel.Success(
            _mapper.Map<IEnumerable<CategoryResponse>>(categories.data)
            , categories.count));
    }
}
