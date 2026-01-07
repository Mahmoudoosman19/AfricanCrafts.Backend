using IdentityHelper.Abstraction;
using Product.Application.Abstractions;

namespace Product.Application.Features.Product.Queries.GetCommentsOnRejectedProducts
{
    //public class GetCommentsOnRejectedProductsByProductIdValidator : AbstractValidator<GetCommentsOnRejectedProductsByProductIdQuery>
    //{
    //    private readonly ITokenExtractor _tokenExtractor;
    //    private readonly IUserManagement _userManagement;
    //    private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
    //    private readonly IVendorService _vendorService;
    //    public GetCommentsOnRejectedProductsByProductIdValidator(IGenericRepository<Domain.Entities.Product> productRepo,
    //       ITokenExtractor tokenExtractor,
    //       IUserManagement userManagement,
    //       IGenericRepository<Domain.Entities.Product> ProductRepo,
    //       IVendorService vendorService)
    //    {
    //        _tokenExtractor = tokenExtractor;
    //        _userManagement = userManagement;
    //        _productRepo = ProductRepo;

    //        RuleFor(x => x.ProductId)
    //            .NotEmpty().WithMessage(Messages.EmptyField);

    //        RuleFor(x => x)
    //            .CustomAsync(VerifyVendor);
    //        _vendorService = vendorService;
    //    }
    //    private async Task VerifyVendor(GetCommentsOnRejectedProductsByProductIdQuery query, ValidationContext<GetCommentsOnRejectedProductsByProductIdQuery> context, CancellationToken cancellationToken)
    //    {
    //        var product = await _productRepo.GetByIdAsync(query.ProductId);
    //        if (product == null)
    //            context.AddFailure(nameof(query.ProductId), Messages.NotFound);
    //        else
    //        {
    //            var userId = _tokenExtractor.GetUserId();
    //            var user = await _userManagement.GetUserData(userId);
    //            if (user.Role == "Vendor" && userId != product.VendorId)
    //                context.AddFailure(Messages.EmptyBadRequest);
    //        }

    //    }
    //}
}
