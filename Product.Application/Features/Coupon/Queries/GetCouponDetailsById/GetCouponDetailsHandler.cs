using IdentityHelper.Abstraction;
using Product.Application.Features.Coupon.Dto;

namespace Product.Application.Features.Coupon.Queries.GetCouponDetailsById
{
    public class GetCouponDetailsHandler : IQueryHandler<GetCouponDetailsQuery , CouponDto >
    {
        private IGenericRepository<Domain.Entities.Coupon> _couponRepo;
        private readonly IMapper _mapper;
        private readonly IUserManagement _userManagement;
        public GetCouponDetailsHandler(IGenericRepository<Domain.Entities.Coupon> couponRepo, IMapper mapper , IUserManagement userManagement)
        {
            _couponRepo = couponRepo;
            _mapper = mapper;
            _userManagement = userManagement;
        }

        public async Task<ResponseModel<CouponDto>> Handle(GetCouponDetailsQuery request, CancellationToken cancellationToken )
        {
             var coupon = await _couponRepo.GetByIdAsync(request.Id);
            var couponDto = await CouponMapping(coupon!);
            // var couponMapping = _mapper.Map<CouponDto>(coupon);
            //return ResponseModel.Success(couponMapping);
           
            return ResponseModel.Success(couponDto);
        }
        private async Task<CouponDto> CouponMapping(Domain.Entities.Coupon coupon)
        {
            string userName = null;
            try
            {
                var user = await _userManagement.GetUserData(coupon.UserId);
                userName = user?.UserName ?? " ";
            }
            catch (Exception ex)
            {
                userName = "";
            }

            return new CouponDto()
            {
                Id = coupon.Id,
                Code = coupon.Code,
                UserCount = coupon.UserCount,
                DiscountPercentage = coupon.DiscountPercentage,
                IsActive = coupon.IsActive,
                StartDate = coupon.StartDate,
                ExpireDate = coupon.ExpireDate,
                CreatedOnUtc = coupon.CreatedOnUtc,
                ModifiedOnUtc = coupon.ModifiedOnUtc,
                UserId = coupon.UserId,
                UserName = userName

            };
           
        }
    }
    }

