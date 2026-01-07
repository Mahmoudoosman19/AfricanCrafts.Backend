using IdentityHelper.Abstraction;
using Product.Application.Features.Review.Queries.GetProductReviews;

namespace Product.Application.Specifications.Reviews
{
    internal class GetReviewsByProductIdAndVendorIdSpecification : Specification<Domain.Entities.Review>
    {
        public GetReviewsByProductIdAndVendorIdSpecification(Guid productId, GetProductReviewsQuery request)
        {
            ApplyPaging(request.PageSize, request.PageIndex);

                    AddCriteria(x => x.ProductId == productId);

            

        }
    }
}
