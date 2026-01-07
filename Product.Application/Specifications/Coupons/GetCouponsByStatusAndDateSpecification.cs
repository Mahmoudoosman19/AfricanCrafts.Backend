using Product.Application.Features.Coupon.Queries.ListCoupon;

namespace Product.Application.Specifications.Coupons
{
    public class GetCouponsByStatusAndDateSpecification : Specification<Domain.Entities.Coupon>
    {
        public GetCouponsByStatusAndDateSpecification(GetCouponsByStatusAndDateQuery request,Guid id)
        {
            if (request.IsActive is not null)
                AddCriteria(c => c.IsActive == request.IsActive);
            if(id!=Guid.Empty)   
                AddCriteria(c=>c.UserId ==id);
            ApplyPaging(request.PageSize,request.PageIndex);
            if (request.Code != null)
            {
                AddCriteria(x => x.Code.Contains(request.Code));
            }
            if (request.DiscountPercentage != null)
            {
                AddCriteria(x => x.DiscountPercentage==(request.DiscountPercentage));
            }

        }
        public GetCouponsByStatusAndDateSpecification(string Code)
        {
            AddCriteria(c=>c.Code.ToLower() == Code.ToLower() && c.IsActive &&c.ExpireDate > DateTime.Now && c.StartDate <= DateTime.Now);
        }
    }
}
