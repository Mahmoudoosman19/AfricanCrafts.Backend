using Bus.Contracts.Enum;
using Bus.Contracts.Models.Notifications;
using Common.Application.Localization;
using IdentityHelper.Abstraction;
using System.Text;

namespace Product.Application.Features.Product.Commands.AddProduct;

internal class AddProductCommandHandler : ICommandHandler<AddProductCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILocalizer _localizer;
    private readonly ITokenExtractor _tokenExtractor;  


    public AddProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILocalizer localizer, ITokenExtractor tokenExtractor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _tokenExtractor = tokenExtractor;
    }

    public async Task<ResponseModel<string>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var userId = _tokenExtractor.GetUserId();  
        var currentLang = _localizer.GetLanguage() ?? "en";
        if (request.ProductCode == null)
            request.ProductCode = GenerateProductCode();
        else
            request.ProductCode = request.ProductCode;

        var product = _mapper.Map<AddProductCommand, Domain.Entities.Product>(request);
        await _unitOfWork.Repository<Domain.Entities.Product>().AddAsync(product, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        List<string> roles = new List<string>
        {
          "Admin",
          "SuperVisor",
          "Vendor"
        };
        List<string> replaceValue = new List<string>
        {
            product.NameAr,
            product.NameEn,
        };
        var stringLang = currentLang.Substring(0, 2);
        
        if (Enum.TryParse(stringLang, true, out LanguageEnum language))
        {
            var notificationsModel = new NotificationsModel
            {
                Message = MessageEnumKey.AddProduct,
                Language = language,
                UserId = userId,
                Roles = roles,
                ReplaceValue = replaceValue 
            };
            //product.CreateNotification(notificationsModel);
        }

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