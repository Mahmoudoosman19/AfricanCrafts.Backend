namespace Product.Application.Specifications.Coupons
{
    internal class GetCouponDetailsByCodeForCustomerSpecification : Specification<Domain.Entities.Coupon>
    {
        public GetCouponDetailsByCodeForCustomerSpecification(string Code)
        {
            AddCriteria(c => c.Code.ToLower() == Code.ToLower());
        }
    }
}
