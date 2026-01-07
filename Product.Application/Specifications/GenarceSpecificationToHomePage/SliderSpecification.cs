using Product.Application.Features.HomePage.Query.Sliders;

namespace Product.Application.Specifications.GenarceSpecificationToHomePage
{
    public class SliderSpecification : Specification<Domain.Entities.Slider>
    {
        public SliderSpecification(GetSliderQuery query)
        {
            AddCriteria(c => c.IsActive);
            ApplyPaging(query.PageSize, query.PageIndex);
            AddInclude(nameof(Domain.Entities.Category));
        }
    }
}
