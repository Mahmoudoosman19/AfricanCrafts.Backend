using IdentityHelper.Abstraction;
using Product.Application.Features.Coupon.Dto;
using Product.Application.Specifications.Coupons;

namespace Product.Application.Features.Coupon.Queries.GetCouponDetailsByCodeForCustomer
{
    internal class GetCouponDetailsByCodeForCustomerQueryHandler : IQueryHandler<GetCouponDetailsByCodeForCustomerQuery, CouponDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserManagement _userManagement;
        public GetCouponDetailsByCodeForCustomerQueryHandler(IUnitOfWork unitOfWork, IUserManagement userManagement)
        {
            _unitOfWork = unitOfWork;
            _userManagement = userManagement;
        }
        public async Task<ResponseModel<CouponDto>> Handle(GetCouponDetailsByCodeForCustomerQuery request, CancellationToken cancellationToken)
        {
            var coupon = _unitOfWork.Repository<Domain.Entities.Coupon>().GetEntityWithSpec(new GetCouponDetailsByCodeForCustomerSpecification(request.Code));
            if (coupon == null)
                return ResponseModel.Failure<CouponDto>(Messages.NotFound);

            var couponDto = await CouponMapping(coupon!);
            return ResponseModel.Success(couponDto);
        }
        private async Task<CouponDto> CouponMapping(Domain.Entities.Coupon coupon)
        {
            string userName = null;
            try
            {
                var user = await _userManagement.GetUserData(coupon.UserId);
                userName = user.UserName;
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
