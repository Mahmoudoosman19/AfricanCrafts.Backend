//using IdentityHelper.Abstraction;
//using Product.Application.Abstractions;

//namespace Product.Application.Features.Review.Commands.UpdateReview
//{
//    internal class AddReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
//    {
//        private readonly ITokenExtractor _tokenExtractor;
//        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
//        private readonly IOrderStatus _orderService;
//        public AddReviewCommandValidator(
//            ITokenExtractor tokenExtractor,
//            IGenericRepository<Domain.Entities.Product> productRepo,
//            IOrderStatus orderService)
//        {
//            _tokenExtractor = tokenExtractor;
//            _productRepo = productRepo;
//            _orderService = orderService;

//            RuleFor(x => x.ProductId)
//                .NotEmpty().WithMessage(Messages.EmptyField)
//                .EntityExist(productRepo).WithMessage(Messages.NotFound)
//                .MustAsync(BeOwnedByVendorIfVendor).WithMessage(Messages.VendorGetProductsReviewDoesNotOwn)
//                .MustAsync(CheckStatus).WithMessage(Messages.YouDoNotHaveThisProductInYourOrder);

//            RuleFor(x => x.Id)
//                .NotEmpty().WithMessage(Messages.EmptyField);

//            RuleFor(x => x.Rate)
//            .NotEmpty().WithMessage(Messages.EmptyField)
//            .GreaterThanOrEqualTo(0.0)
//            .LessThanOrEqualTo(5.0);
//        }

//        private async Task<bool> BeOwnedByVendorIfVendor(Guid productId, CancellationToken cancellationToken)
//        {
//            var userId = _tokenExtractor.GetUserId();
//            var userRole = _tokenExtractor.GetUserRole();

//            if (userRole == "Vendor")
//            {
//                var product = await _productRepo.GetByIdAsync(productId);
//                if (product == null || product.VendorId != userId)
//                    return false;
//            }
//            return true;
//        }

//        private async Task<bool> CheckStatus(Guid productId, CancellationToken cancellationToken)
//        {
//            var customerId = _tokenExtractor.GetUserId();
//            var productStatus = await _orderService.GetOrderStatus(productId, customerId);
//            var product = await _productRepo.GetByIdAsync(productId);
//            if (product == null || productStatus == false)
//                return false;
//            return true;
//        }
//    }
//}
