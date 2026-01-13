//using CacheHelper;
//using CacheHelper.Abstraction;
//using CacheHelper.Keys;
//using ImageKitFileManager.Helpers;
//using Product.Application.Features.Categories.Dto;
//using Product.Application.Specifications.Categories;
//using Product.Domain.Abstraction;
//using Product.Domain.Entities;

//namespace Product.Application.Features.Categories.Queries.GetCategoriesForest;

//internal class GetCategoriesForestQueryHandler : IQueryHandler<GetCategoriesForestQuery, GetCategoriesForestQueryResponse>
//{
//    private readonly IProductRepository<Category> _categoryRepo;
//    private readonly CacheContext _cacheService;

//    public GetCategoriesForestQueryHandler(
//        IProductRepository<Category> categoryRepo,
//        ICacheStrategy cacheStrategy)
//    {
//        _categoryRepo = categoryRepo;
//        _cacheService = new CacheContext(cacheStrategy);
//    }


//    public async Task<ResponseModel<GetCategoriesForestQueryResponse>> Handle(GetCategoriesForestQuery request, CancellationToken cancellationToken)
//    {
//        var result = await _cacheService.GetOrSetCacheDataAsync(
//            CacheKeys.CategoryForest,
//            () =>
//            {
//                var activeCategories = _categoryRepo.GetWithSpec(new GetActiveCategoriesSpecification()).data.ToList();
//                var forest = BuildCategoryForest(activeCategories);
//                return Task.FromResult(forest);
//            });

//        var response = new GetCategoriesForestQueryResponse
//        {
//            Categories = result!,
//        };

//        return response;
//    }

//    private List<CategoryTreeDto> BuildCategoryForest(IReadOnlyList<Category> allCategories)
//    {
//        var rootCategories = allCategories.Where(c => c.ParentId == null).ToList();
//        return rootCategories
//            .Select(rootCategory => BuildCategoryTree(rootCategory, allCategories))
//            .ToList();
//    }

//    private CategoryTreeDto BuildCategoryTree(Category rootCategory, IReadOnlyList<Category> allCategories)
//    {
//        var categoryPath = GetCategoryPath(rootCategory, allCategories);

//        return new CategoryTreeDto
//        {
//            Id = rootCategory.Id,
//            NameAr = rootCategory.NameAr,
//            NameEn = rootCategory.NameEn,
//            Image = ImageKitBaseUrl.GenerateImageUrl(rootCategory.ImageName, ImageKitFileManager.Enums.FileType.Category),
//            ImageFileId = rootCategory.ImageFileId,
//            SizeGroupId = rootCategory.SizeGroupId,
//            CreatedOnUtc = rootCategory.CreatedOnUtc,
//            ModifiedOnUtc = rootCategory.ModifiedOnUtc,
//            CategoryPath = categoryPath,
//            SubCategories = GetSubCategoryTrees(rootCategory.Id, allCategories)
//        };
//    }

//    private List<CategoryTreeDto> GetSubCategoryTrees(Guid parentId, IReadOnlyList<Category> allCategories)
//    {
//        var subCategories = allCategories.Where(x => x.ParentId == parentId).ToList();

//        return subCategories
//            .Select(subCategory => BuildCategoryTree(subCategory, allCategories))
//            .ToList();
//    }

//    private string GetCategoryPath(Category category, IReadOnlyList<Category> allCategories)
//    {
//        if (category.ParentId == null)
//            return category.NameEn;

//        var parentCategory = allCategories.FirstOrDefault(c => c.Id == category.ParentId);

//        if (parentCategory == null)
//            throw new InvalidOperationException($"Parent category not found for ID: {category.ParentId}");

//        return $"{GetCategoryPath(parentCategory, allCategories)}/{category.NameEn}";
//    }
//}
