using Product.Application.Features.Categories.Queries.GetListCategory;
using Product.Domain.Entities;

namespace Product.Application.Specifications.Categories
{
    internal class GetCategoryByStatusWithFilterSearchWithSizeGroupAndParentCategorySpecification : Specification<Category>
    {
        public GetCategoryByStatusWithFilterSearchWithSizeGroupAndParentCategorySpecification(GetCategoryByStatusWithFilterSearchQuery request)
        {
            if (request.Search is not null)
                AddCriteria(c => c.NameAr.Contains(request.Search) || c.NameEn.Contains(request.Search));
            if (request.IsActive is not null)
                AddCriteria(c => c.IsActive == request.IsActive);
            AddInclude(nameof(Category.ParentCategory));
            AddInclude(nameof(Category.SizeGroup));
            ApplyPaging(request.PageSize, request.PageIndex);
        }
    }
}
