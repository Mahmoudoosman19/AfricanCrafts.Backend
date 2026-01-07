namespace Product.Application.Features.Sliders.Queries.GetListSlider
{
    public class GetSliderByStatusWithCategoryQuery:IQuery<IEnumerable<SliderResponse>>
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        public bool? IsActive { get; set; }
        public Guid? UserId { get; set; }
    }
}
