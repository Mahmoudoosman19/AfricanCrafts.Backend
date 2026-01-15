using Bus.Contracts.Enum;
using Bus.Contracts.Models.Notifications;
using Common.Application.Localization;
using IdentityHelper.Abstraction;
using ImageKitFileManager.Abstractions;
using Product.Domain.Abstraction;
using Product.Domain.Entities;
using System.Text;

namespace Product.Application.Features.Product.Commands.AddProduct;

internal class AddProductCommandHandler : ICommandHandler<AddProductCommand>
{
    private readonly IMapper _mapper;
    private readonly IProductUnitOfWork _unitOfWork;
    private readonly IImageKitService _imageKitService; 

    public AddProductCommandHandler(
        IMapper mapper,
        IProductUnitOfWork unitOfWork,
        IImageKitService imageKitService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _imageKitService = imageKitService;
    }

    public async Task<ResponseModel> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.ProductCode))
            request.ProductCode = GenerateProductCode();
        var product = _mapper.Map<Domain.Entities.Product>(request);
        if (request.Images != null && request.Images.Any())
        {
            foreach (var imgDto in request.Images)
            {
                if (imgDto.ImageFile != null)
                {
                    var newImage = new ProductImage();

                    newImage.SetColor(imgDto.ColorCode);

                    newImage.SetImage(_imageKitService, imgDto.ImageFile, product.ImagesFolderName);

                    product.AddImage(newImage);
                }
            }
        }

        await _unitOfWork.Repository<Domain.Entities.Product>().AddAsync(product, cancellationToken);
        var result = await _unitOfWork.CompleteAsync(cancellationToken);

        return result > 0
            ? ResponseModel.Success(Messages.SuccessfulOperation)
            : ResponseModel.Failure("حدث خطأ أثناء حفظ المنتج");
    }

    private string GenerateProductCode()
    {
        return "PRD-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    }
}