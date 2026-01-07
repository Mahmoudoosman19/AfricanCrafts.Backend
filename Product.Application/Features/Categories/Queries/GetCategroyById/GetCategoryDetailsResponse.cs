using Product.Application.SharedDTOs.Category;

namespace Product.Application.Features.Categories.Queries.GetCategroyById
{
    public class GetCategoryDetailsResponse
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; private set; }
        public bool IsActive { get; set; } = false;
        public string ImageUrl { get; set; }
        public Guid SizeGroupId { get; set; }
        public string SizeGroupNameAr { get; set; }
        public string SizeGroupNameEn { get; set; }
        public CategoryLookupDto? ParentCategory { get; set; } = new();

    }

}
