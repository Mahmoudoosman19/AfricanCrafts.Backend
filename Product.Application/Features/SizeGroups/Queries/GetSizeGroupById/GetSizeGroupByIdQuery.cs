namespace Product.Application.Features.SizeGroups.Queries.GetSizeGroupById
{
    public sealed record GetSizeGroupByIdQuery : IQuery<SizeGroupWithSizesResponse>
    {
        public Guid Id { get; init; }
    }
}