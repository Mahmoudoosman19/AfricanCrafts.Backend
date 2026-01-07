namespace Product.Application.Features.HomePage.Query.Sliders
{
    public class GetSliderQuery : IQuery<IEnumerable<GetSliderQueryResponse>>
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
    }
}
