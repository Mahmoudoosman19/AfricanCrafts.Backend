namespace Product.Application.Features.Advertisements.Queries.GetList
{
    public class GetAdvertisementBaseStatusAndRoleQuery : IQuery<IEnumerable<AdvertisementQueryResponse>>
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        public bool? IsActive { get; set; }
        public Guid? UserId { get; set; }

    }
}
