namespace Product.Application.Features.Advertisements.Queries.GetList
{
    public class AdvertisementQueryResponse
    {
        public Guid Id { get; set; }
        public string NameAr { get; private set; } = null!;
        public string NameEn { get; private set; } = null!;
        public string ImageName { get; private set; } = null!;
        public string DescriptionAr { get; private set; } = null!;
        public string DescriptionEn { get; private set; } = null!;
        public bool IsActive { get; private set; } = false;
        public string AdvertisementUrl { get; set; } = null!;
    }
}
