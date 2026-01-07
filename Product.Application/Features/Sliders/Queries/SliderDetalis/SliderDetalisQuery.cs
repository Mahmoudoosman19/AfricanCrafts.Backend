using Product.Application.Features.Sliders.Queries.GetListSlider;

namespace Product.Application.Features.Sliders.Queries.SliderDetalis
{
    public class SliderDetalisQuery:IQuery<SliderResponse>
    {
        public Guid Id { get; set; }    
    }
}
