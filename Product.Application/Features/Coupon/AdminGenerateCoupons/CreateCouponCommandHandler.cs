using IdentityHelper.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Coupon.AdminGenerateCoupons
{
    public class CreateCouponCommandHandler : ICommandHandler<CreateCouponCommand>
    {
        private readonly IGenericRepository<Domain.Entities.Coupon> _couponRepo;
        private readonly IMapper _mapper;
        private readonly ITokenExtractor _userManager;

        public CreateCouponCommandHandler(IMapper mapper, ITokenExtractor userManager,IGenericRepository<Domain.Entities.Coupon> couponRepo)
        {
            _mapper = mapper;
            _userManager = userManager;
            _couponRepo = couponRepo;
        }


        public async Task<ResponseModel> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var userId = _userManager.GetUserId();
            var coupons = new List<Domain.Entities.Coupon>();
            for (var i = 1; i <= request.NumOfCoupons; i++)
            {
                var coupon = _mapper.Map<Domain.Entities.Coupon>(request);
                coupon.GenerateCode();
                coupon.SetUser(userId);
                if (request.ProductIds != null)
                {
                    var productCoupons = request.ProductIds
                      .Select(productId =>
                      {
                          var productCoupon = new CouponProducts();
                          productCoupon.SetProductId(productId);
                          return productCoupon;
                      }).ToList();

                    coupon.AddRangeProductCoupons(productCoupons);
                }
                coupons.Add(coupon);
            }
                await _couponRepo.AddRangeAsync(coupons);
               await _couponRepo.SaveChangesAsync();
            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
