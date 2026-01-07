using Product.Application.Specifications.Categories;

namespace Product.Application.Features.Categories.Queries.GetParentCategories;

internal class GetParentCategoriesQueryHandler :
    IQueryHandler<GetParentCategoriesQuery, IEnumerable<CategoryResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Category> _categoryRepo;
    private readonly IMapper _mapper;

    public GetParentCategoriesQueryHandler(
        IGenericRepository<Domain.Entities.Category> categoryRepo
        , IMapper mapper)
    {
        _categoryRepo = categoryRepo;
        _mapper = mapper;
    }
    public Task<ResponseModel<IEnumerable<CategoryResponse>>> Handle(GetParentCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = _categoryRepo.GetWithSpec(new GetParentCategoriesSpecification(request));

        if (categories.data == null)
            return Task.FromResult(ResponseModel.Success<IEnumerable<CategoryResponse>>([], Messages.NoCategoriesAreFound));

        return Task.FromResult(ResponseModel.Success(
            _mapper.Map<IEnumerable<CategoryResponse>>(categories.data)
            , categories.count));

    }
}
