namespace Product.Application.Features.Sizes.Queries.GetSizesBySizeGroupId
{
    public class GetSizesBySizeGroupIdQuery : IQuery<IReadOnlyList<GetSizesResponse>>
    {
        public Guid Id { get; set; }

    }
}
