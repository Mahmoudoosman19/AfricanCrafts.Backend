

using ImageKitFileManager.Abstractions;
using Product.Domain.Entities;

namespace Product.Application.Features.Categories.Commands.AddCategory
{
    internal sealed class AddCategoryCommandHandler : ICommandHandler<AddCategoryCommand>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IImageKitService _imageKitService;
        public AddCategoryCommandHandler(IGenericRepository<Category> categoryRepo,
            IMapper mapper,
            IImageKitService imageKitService)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _imageKitService = imageKitService;
        }

        public async Task<ResponseModel> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            var imageKitResponse = await _imageKitService.UploadFileAsync(request.Image, ImageKitFileManager.Enums.FileType.Category);
            if (!imageKitResponse.Success)
                return ResponseModel.Failure(imageKitResponse.Message);
            category.SetImage(imageKitResponse.Name, imageKitResponse.FileId);
            await _categoryRepo.AddAsync(category);
            await _categoryRepo.SaveChangesAsync();
            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
