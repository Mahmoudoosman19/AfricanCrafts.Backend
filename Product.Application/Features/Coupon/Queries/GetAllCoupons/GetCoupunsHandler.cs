using IdentityHelper.Abstraction;
using Product.Application.Features.Coupon.AdminGenerateCoupons.Specifications;
using Product.Application.Features.Coupon.Dto;

namespace Product.Application.Features.Coupons.Query
{
    public class GetCoupunsHandler : IQueryHandler<GetAllCouponsQuery, List<CouponDto>>
    {
        private IGenericRepository<Domain.Entities.Coupon> _couponRepo;
        private readonly IMapper _mapper;
        private readonly IUserManagement _UserManagement;


        public GetCoupunsHandler(IMapper mapper, IGenericRepository<Domain.Entities.Coupon> couponRepo, IUserManagement UserManagement)
        {
            _couponRepo = couponRepo;
            _UserManagement = UserManagement;
        }
        public async Task<ResponseModel<List<CouponDto>>> Handle(GetAllCouponsQuery request, CancellationToken cancellationToken)
        {
            var (coupons, count) = _couponRepo.GetWithSpec(new GetCouponsOrderByDateSpecification(request));

            _couponRepo.ExecuteUpdateRange(c => c.ExpireDate < DateTime.UtcNow && c.IsActive, x => x.SetProperty(c => c.IsActive, false));

            _couponRepo.ExecuteUpdateRange(c =>
            c.StartDate <= DateTime.UtcNow &&
            c.ExpireDate >= DateTime.UtcNow &&
            !c.IsActive &&
            !c.IsManuallyDeactivated,
            x => x.SetProperty(c => c.IsActive, true));

            var MappingCoupons = new List<CouponDto>();

            foreach (var coupon in coupons)
            {
                MappingCoupons.Add(await CouponMapping(coupon));
            }

            return ResponseModel.Success(MappingCoupons, count);
        }
        private async Task<CouponDto> CouponMapping(Domain.Entities.Coupon coupon)
        {
            string userName = null;
            try
            {
                var user = await _UserManagement.GetUserData(coupon.UserId);
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