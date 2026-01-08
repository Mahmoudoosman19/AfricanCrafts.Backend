using ImageKitFileManager.Abstractions;
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Categories.Commands.UpdateCategory
{
    internal class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand>
    {
        private readonly IProductRepository<Category> _categoryRepo;
        private readonly IImageKitService _imageKitService;

        public UpdateCategoryCommandHandler(IProductRepository<Category> categoryRepo, IImageKitService imageKitService)
        {
            _categoryRepo = categoryRepo;
            _imageKitService = imageKitService;
        }

        public async Task<ResponseModel> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepo.GetByIdAsync(request.Id);

            await UpdatedCategoryProps(category!, request);

            if (category!.IsActive)
                category.UpdateCachedForest();

            await _categoryRepo.SaveChangesAsync();
            return ResponseModel.Success(Messages.SuccessfulOperation);
        }

        private async Task UpdatedCategoryProps(Category category, UpdateCategoryCommand request)
        {
            category!.SetName(request.NameAr, request.NameEn);
            category.SetSizeGroup(request.SizeGroupId);
            category.SetParentCategory(request.ParentId);
            if (request.Image != null)
            {
                var imageKitResponse = await _imageKitService.UpdateFileAsync(request.Image, category.ImageFileId, ImageKitFileManager.Enums.FileType.Category);
                if (imageKitResponse != null)
                    category.SetImage(imageKitResponse.Name, imageKitResponse.FileId);
            }
        }
    }
}

