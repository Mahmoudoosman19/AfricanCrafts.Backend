using Product.Application.Features.HomePage.Query.Categories;
using Product.Domain.Entities;

namespace Product.Application.Specifications.GenarceSpecificationToHomePage
{
    internal class GetCategoryByStatusWithSizeGroupAndParentCategorySpecification : Specification<Category>
    {
        public GetCategoryByStatusWithSizeGroupAndParentCategorySpecification(GetCategoriesQuery query)
        {
            AddCriteria(c => c.IsActive == true);

            AddInclude(nameof(Category.ParentCategory));

            AddInclude(nameof(Category.SizeGroup));

            ApplyPaging(query.PageSize, query.PageIndex);
        }
    }
}
