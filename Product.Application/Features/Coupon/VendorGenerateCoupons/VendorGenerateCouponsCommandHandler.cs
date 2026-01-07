using IdentityHelper.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Coupon.VendorGenerateCoupons
{
    public class VendorGenerateCouponsCommandHandler : ICommandHandler<VendorGenerateCouponsCommand>
    {
        private readonly IGenericRepository<Domain.Entities.Coupon> _couponRepo;
        private readonly IMapper _mapper;
        private readonly ITokenExtractor _tokenExtractor;
        public VendorGenerateCouponsCommandHandler(IMapper mapper
            , ITokenExtractor tokenExtractor
            , IGenericRepository<Domain.Entities.Coupon> couponRepo)
        {
            _mapper = mapper;
            _tokenExtractor = tokenExtractor;
            _couponRepo = couponRepo;
        }
        public async Task<ResponseModel> Handle(VendorGenerateCouponsCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenExtractor.GetUserId();
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

