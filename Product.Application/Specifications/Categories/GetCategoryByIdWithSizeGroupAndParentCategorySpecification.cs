using Product.Domain.Entities;

namespace Product.Application.Specifications.Categories
{
    internal class GetCategoryByIdWithSizeGroupAndParentCategorySpecification : Specification<Category>
    {
        public GetCategoryByIdWithSizeGroupAndParentCategorySpecification(Guid id)
        {
            AddCriteria(c => c.Id == id);
            AddInclude(nameof(Category.SizeGroup));
            AddInclude(nameof(Category.ParentCategory));

        }
    }

}
