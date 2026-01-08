using ImageKitFileManager.Abstractions;
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Sliders.Commands.AddSlider
{
    internal class AddSliderCommandHandelr : ICommandHandler<AddSliderCommand>
    {

        private readonly IMapper _mapper;
        private readonly IProductRepository<Slider> _SliderRepo;

        private readonly IImageKitService _imageKitService;

        public AddSliderCommandHandelr(IProductRepository<Slider> sliderRepo, IMapper mapper, IImageKitService imageKitService)
        {
            _SliderRepo = sliderRepo;
            _mapper = mapper;
            _imageKitService = imageKitService;
        }
        public async Task<ResponseModel> Handle(AddSliderCommand request, CancellationToken cancellationToken)
        {


            var Slider = new Slider();
            Slider.setName(request.NameAr, request.NameEn);
            var imageUploadResult = await _imageKitService.UploadFileAsync(request.Image, ImageKitFileManager.Enums.FileType.Slider, Guid.NewGuid());
            Slider.SetImage(imageUploadResult.Name, imageUploadResult.FileId);
            Slider.SetCategoryId(request.CategoryId);
            await _SliderRepo.AddAsync(Slider);
            await _SliderRepo.SaveChangesAsync();
            return ResponseModel.Success(Messages.SuccessfulOperation);
        }

    }
}

