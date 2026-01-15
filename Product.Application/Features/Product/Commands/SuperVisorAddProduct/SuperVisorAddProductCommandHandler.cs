using IdentityHelper.Abstraction;
using ImageKitFileManager.Abstractions;
using Product.Application.Features.Product.Commands.SuperVisorAddProduct;
using Product.Domain.Abstraction;
using Product.Domain.Enums;
using System.Text;

namespace Product.Application.Features.Product.Commands.AddProduct
{
    internal class SupervisorAddProductCommandHandler : ICommandHandler<SupervisorAddProductCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IProductUnitOfWork _unitOfWork;
        private readonly ITokenExtractor _userManager;
        private readonly IImageKitService _imageKitService;

        public SupervisorAddProductCommandHandler(IMapper mapper, IProductUnitOfWork unitOfWork, ITokenExtractor userManager,
            IImageKitService imageKitService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _imageKitService = imageKitService;
        }

        public async Task<ResponseModel<string>> Handle(SupervisorAddProductCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.ProductCode))
            {
                request.ProductCode = GenerateProductCode();
            }

            var product = _mapper.Map<Domain.Entities.Product>(request);

            foreach (var imgDto in request.Images)
            {
                var entityImage = product.Images.FirstOrDefault(x => x.ColorCode == imgDto.ColorCode);
                if (entityImage != null && imgDto.ImageFile != null)
                {
                    entityImage.SetImage(_imageKitService, imgDto.ImageFile, product.ImagesFolderName);
                }
            }

            await _unitOfWork.Repository<Domain.Entities.Product>().AddAsync(product, cancellationToken);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
                return ResponseModel.Failure<string>("حدث خطأ أثناء حفظ المنتج");

            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
        public static string GenerateProductCode()
        {
            var length = 8;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder randomCode = new StringBuilder();

            Random random = new Random();
            for (int i = 0; i < length; i++)
                randomCode.Append(chars[random.Next(chars.Length)]);

            return randomCode.ToString();
        }
    }
}
