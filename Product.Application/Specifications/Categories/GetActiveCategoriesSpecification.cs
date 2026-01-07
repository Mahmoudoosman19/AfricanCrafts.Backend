using Product.Domain.Entities;

namespace Product.Application.Specifications.Categories
{
    public class GetActiveCategoriesSpecification : Specification<Category>
    {
        public GetActiveCategoriesSpecification()
        {
            AddCriteria(c => c.IsActive==true);
            AddInclude(("SizeGroup"));
            AddInclude("ParentCategory");
            AddInclude("SubCategories");
        }
    }
}
