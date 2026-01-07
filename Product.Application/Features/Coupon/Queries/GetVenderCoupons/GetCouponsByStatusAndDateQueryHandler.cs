using IdentityHelper.Abstraction;
using Product.Application.Specifications.Coupons;

namespace Product.Application.Features.Coupon.Queries.ListCoupon
{
    public class GetCouponsByStatusAndDateQueryHandler : IQueryHandler<GetCouponsByStatusAndDateQuery, IEnumerable<CouponResponse>>
    {
        private readonly IGenericRepository<Domain.Entities.Coupon> _repository;
        private readonly IMapper _mapper;
        private readonly ITokenExtractor _userManager;


        public GetCouponsByStatusAndDateQueryHandler(IGenericRepository<Domain.Entities.Coupon> repository, IMapper mapper, ITokenExtractor userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<ResponseModel<IEnumerable<CouponResponse>>> Handle(GetCouponsByStatusAndDateQuery request, CancellationToken cancellationToken)

        {
            var userId = _userManager.GetUserId();
            var (couponsQuery, count) = _repository.GetWithSpec(new GetCouponsByStatusAndDateSpecification(request, userId));
            _repository.ExecuteUpdateRange(c => c.ExpireDate < DateTime.UtcNow && c.IsActive, x => x.SetProperty(c => c.IsActive, false));
            _repository.ExecuteUpdateRange(c => c.StartDate <= DateTime.UtcNow && c.ExpireDate >= DateTime.UtcNow && !c.IsActive, x => x.SetProperty(c => c.IsActive, true));
            var coupons = _mapper.Map<IEnumerable<CouponResponse>>(couponsQuery);
            return ResponseModel.Success(coupons, count);
        }
    }
}
