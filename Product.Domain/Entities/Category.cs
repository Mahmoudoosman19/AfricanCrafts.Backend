using Common.Domain.Primitives;
using Product.Domain.DomainEvents;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class Category : AggregateRoot<Guid>, IAuditableEntity
    {
        private readonly List<Category> _subCategories = new();

        public string NameAr { get; private set; } = null!;
        public string NameEn { get; private set; } = null!;
        public Guid? ParentId { get; private set; }
        [ForeignKey(nameof(ParentId))]
        public virtual Category? ParentCategory { get; private set; }
        public bool IsActive { get; private set; } = false;
        public string ImageName { get; private set; } = null!;
        public string ImageFileId { get; set; } = null!;
        public Guid? SizeGroupId { get; private set; }
        [ForeignKey(nameof(SizeGroupId))]
        public virtual SizeGroup? SizeGroup { get; private set; }
        public ICollection<Category> SubCategories => _subCategories;
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public void SetName(string nameAr, string nameEn)
        {
            NameAr = nameAr;
            NameEn = nameEn;

        }

        public void SetImage(string imageName, string imageFileId)
        {
            ImageName = imageName;
            ImageFileId = imageFileId;
        }

        public void SetSizeGroup(Guid sizeGroupId)
        {
            SizeGroupId = sizeGroupId;
        }

        public void SetParentCategory(Guid? parentId)
        {
            ParentId = parentId;
        }

        public void SetActivation(bool isActive)
        {
            IsActive = isActive;
        }

        public void AddSubCategory(Category subCategory)
        {
            if (subCategory == null) throw new ArgumentNullException(nameof(subCategory));
            _subCategories.Add(subCategory);
        }
        public void AddRangeSubCategories(List<Category> subCategories)
        {
            if (subCategories == null) throw new ArgumentNullException(nameof(subCategories));
            _subCategories.AddRange(subCategories);
        }

        public void RemoveSubCategory(Category subCategory)
        {
            if (subCategory == null) throw new ArgumentNullException(nameof(subCategory));
            _subCategories.Remove(subCategory);
        }

        public void UpdateCachedForest()
        {
            RaiseDomainEvent(new UpdateCachedCategoryForestEvent(Guid.NewGuid(), Id));
        }
    }
}
