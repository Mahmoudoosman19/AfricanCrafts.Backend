namespace Product.Application.Features.Advertisements.Queries.GetAllAdvertisment
{
    public class GetAllAdvertisementImageByStatusQuery : IQuery<IEnumerable<GetAllAdvertismentImageResponse>>
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        public bool? IsActive { get; set; }
        public Guid? UserId { get; set; }
    }
}
