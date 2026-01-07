using IdentityHelper.Abstraction;
using Product.Application.Features.Product.Commands.SuperVisorAddProduct;
using Product.Domain.Enums;
using System.Text;

namespace Product.Application.Features.Product.Commands.AddProduct
{
    internal class SuperVisorAddProductCommandHandler : ICommandHandler<SuperVisorAddProductCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenExtractor _userManager;

        public SuperVisorAddProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ITokenExtractor userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ResponseModel<string>> Handle(SuperVisorAddProductCommand request, CancellationToken cancellationToken)
        {
            if (request.ProductCode == null)
                request.ProductCode = GenerateProductCode();
            else
                request.ProductCode = request.ProductCode;
            var product = _mapper.Map<SuperVisorAddProductCommand, Domain.Entities.Product>(request);

            product.SetStatus(ProductStatus.Approved);

            await _unitOfWork.Repository<Domain.Entities.Product>().AddAsync(product, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);

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
