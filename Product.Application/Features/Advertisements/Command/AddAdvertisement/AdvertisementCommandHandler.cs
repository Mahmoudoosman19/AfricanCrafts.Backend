using ImageKitFileManager.Abstractions;

namespace Product.Application.Features.Advertisements.Command.AddAdvertisement
{
    internal class AdvertisementCommandHandler : ICommandHandler<AddAdvertisementCommand>
    {
        private readonly IGenericRepository<Domain.Entities.Advertisement> _advertisementRepository;
        private readonly IMapper _IMapper;
        private readonly IImageKitService _imageKitService;


        public AdvertisementCommandHandler(IGenericRepository<Domain.Entities.Advertisement> repository, IMapper mapper, IImageKitService imageKitService)
        {
            _advertisementRepository = repository;
            _IMapper = mapper;
            _imageKitService = imageKitService;
        }



        public async Task<ResponseModel> Handle(AddAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var advertisement = new Domain.Entities.Advertisement();
            advertisement.SetName(request.NameAr, request.NameEn);
            var imageUploadResult = await _imageKitService.UploadFileAsync(request.ImageUrl, ImageKitFileManager.Enums.FileType.Advertisement, Guid.NewGuid());
            if (!imageUploadResult.Success)
                return ResponseModel.Failure(imageUploadResult.Message);
            advertisement.SetImage(imageUploadResult.Name, imageUploadResult.FileId);
            advertisement.SetDescription(request.DescriptionAr, request.DescriptionEn);
            advertisement.SetAdvertisementUrl(request.AdvertisementUrl);
            await _advertisementRepository.AddAsync(advertisement);
            await _advertisementRepository.SaveChangesAsync();
            return ResponseModel.Success(Messages.SuccessfulOperation);

        }
    }
}
