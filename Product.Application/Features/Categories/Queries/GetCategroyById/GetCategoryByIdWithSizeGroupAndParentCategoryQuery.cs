using Product.Application.Features.Categories.Queries.GetCategroyById;

namespace Product.Application.Features.Categories.Queries.GetOneCategroy
{
    public sealed class GetCategoryByIdWithSizeGroupAndParentCategoryQuery : IQuery<GetCategoryDetailsResponse>
    {
        public Guid Id { get; init; }
    }

}
