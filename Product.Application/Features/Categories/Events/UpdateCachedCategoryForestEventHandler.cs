//using CacheHelper;
//using CacheHelper.Keys;
//using CacheHelper.Services;
//using ImageKitFileManager.Helpers;
//using Product.Application.Features.Categories.Dto;
//using Product.Application.Specifications.Categories;
//using Product.Domain.DomainEvents;
//using Product.Domain.Entities;

//namespace Product.Application.Features.Product.Events
//{
//    internal sealed class UpdateCachedCategoryForestEventHandler
//        : IDomainEventHandler<UpdateCachedCategoryForestEvent>
//    {
//        private readonly IGenericRepository<Category> _categoryRepo;
//        private readonly CacheContext _cacheService;

//        public UpdateCachedCategoryForestEventHandler(
//            IGenericRepository<Category> categoryRepo,
//            MemoryCacheService memoryCacheService)
//        {
//            _categoryRepo = categoryRepo;
//            _cacheService = new CacheContext(memoryCacheService);
//        }

//        public async Task Handle(UpdateCachedCategoryForestEvent notification, CancellationToken cancellationToken)
//        {
//            await _cacheService.GetOrSetCacheDataAsync(
//                    CacheKeys.CategoryForest,
//                    () =>
//                    {
//                        var activeCategories = _categoryRepo.GetWithSpec(new GetActiveCategoriesSpecification()).data.ToList();
//                        var forest = BuildCategoryForest(activeCategories);
//                        return Task.FromResult(forest);
//                    }, forceUpdate: true);
//        }

//        private List<CategoryTreeDto> BuildCategoryForest(IReadOnlyList<Category> allCategories)
//        {
//            var rootCategories = allCategories.Where(c => c.ParentId == null).ToList();

//            return rootCategories
//                .Select(rootCategory => BuildCategoryTree(rootCategory, allCategories))
//                .ToList();
//        }

//        private CategoryTreeDto BuildCategoryTree(Category rootCategory, IReadOnlyList<Category> allCategories)
//        {
//            var categoryPath = GetCategoryPath(rootCategory, allCategories);

//            return new CategoryTreeDto
//            {
//                Id = rootCategory.Id,
//                NameAr = rootCategory.NameAr,
//                NameEn = rootCategory.NameEn,
//                Image = ImageKitBaseUrl.GenerateImageUrl(rootCategory.ImageName, ImageKitFileManager.Enums.FileType.Category),
//                ImageFileId = rootCategory.ImageFileId,
//                SizeGroupId = rootCategory.SizeGroupId,
//                CreatedOnUtc = rootCategory.CreatedOnUtc,
//                ModifiedOnUtc = rootCategory.ModifiedOnUtc,
//                CategoryPath = categoryPath,
//                SubCategories = GetSubCategoryTrees(rootCategory.Id, allCategories)
//            };
//        }

//        private List<CategoryTreeDto> GetSubCategoryTrees(Guid parentId, IReadOnlyList<Category> allCategories)
//        {
//            var subCategories = allCategories.Where(x => x.ParentId == parentId).ToList();

//            return subCategories
//                .Select(subCategory => BuildCategoryTree(subCategory, allCategories))
//                .ToList();
//        }

//        private string GetCategoryPath(Category category, IReadOnlyList<Category> allCategories)
//        {
//            if (category.ParentId == null)
//                return category.NameEn;

//            var parentCategory = allCategories.FirstOrDefault(c => c.Id == category.ParentId);

//            if (parentCategory == null)
//                throw new InvalidOperationException($"Parent category not found for ID: {category.ParentId}");

//            return $"{GetCategoryPath(parentCategory, allCategories)}/{category.NameEn}";
//        }
//    }
//}
