namespace Product.Application.Features.Sizes.Queries.GetSizes
{
    public class GetSizesQueryResponse
    {
        public Guid Id { get; set; }
        public string NameAr { get; private set; }
        public string NameEn { get; private set; }
        public bool IsActive { get; set; }
    }
}
