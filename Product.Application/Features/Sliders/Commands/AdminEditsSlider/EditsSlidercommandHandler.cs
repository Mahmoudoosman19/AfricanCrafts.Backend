using ImageKitFileManager.Abstractions;
using Product.Domain.Entities;

namespace Product.Application.Features.Sliders.Commands.AdminEditsSlider
{
    internal class EditsSlidercommandHandler:ICommandHandler<EditsSlidercommand>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Domain.Entities.Slider> _sliderRepo;
        private readonly IImageKitService _imageKitService;

        public EditsSlidercommandHandler(IGenericRepository<Domain.Entities.Slider> sliderRepo, IMapper mapper, IImageKitService imageKitService)
        {
            _sliderRepo = sliderRepo;
            _mapper = mapper;
            _imageKitService = imageKitService;
        }

        public async Task<ResponseModel> Handle(EditsSlidercommand request, CancellationToken cancellationToken)
        {
            var slider = await _sliderRepo.GetByIdAsync(request.Id);
            await UpdatedSliderProps(slider!, request);
            await _sliderRepo.SaveChangesAsync();
            return ResponseModel.Success(Messages.SuccessfulOperation);
        }

        private async Task UpdatedSliderProps(Slider slider, EditsSlidercommand request)
        {
            slider!.setName(request.NameAr, request.NameEn);
            slider!.SetCategoryId(request.CategoryId);
            if (request.Image != null)
            {
                var imageKitResponse = await _imageKitService.UpdateFileAsync(request.Image, slider.ImageFileId, ImageKitFileManager.Enums.FileType.Slider);
                if (imageKitResponse != null)
                    slider.SetImage(imageKitResponse.Name, imageKitResponse.FileId);
            }
        }
    }
}

