using Product.Application.Features.Sliders.Queries.GetListSlider;

namespace Product.Application.Specifications.slider
{
    public class GetSliderByStatusWithCategorySpecification: Specification<Domain.Entities.Slider>
    {
        public GetSliderByStatusWithCategorySpecification(GetSliderByStatusWithCategoryQuery request)
        {
            if (request.IsActive is not null)
                AddCriteria(c => c.IsActive == request.IsActive);
            AddCriteria(c => c.IsDeleted == false);
            ApplyPaging(request.PageSize, request.PageIndex);
            AddInclude(nameof(Domain.Entities.Category));

         

        }

    }
}
