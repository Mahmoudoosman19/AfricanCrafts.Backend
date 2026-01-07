namespace Product.Application.Features.Sizes.Queries.GetSizes
{
    public class GetSizesByStatusQuery :IQuery<List<GetSizesQueryResponse>>
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        public bool? IsActive { get; set; } 
    }
}
