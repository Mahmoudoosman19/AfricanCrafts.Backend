namespace Product.Application.Features.HomePage.Query.Sliders
{
    internal class GetSliderQueryResponse
    {
        public Guid Id { get; set; }
        public string NameAr { get; private set; } = null!;
        public string NameEn { get; private set; } = null!;
        public string ImageUrl { get; private set; }
        public bool IsActive { get; private set; }
        public Guid? CategoryId { get; private set; }
        public string CategoryNameEn { get; private set; }
        public string CategoryNameAr { get; private set; }
    }
}
