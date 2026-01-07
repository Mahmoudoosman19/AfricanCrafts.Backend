using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class Slider : Entity<Guid>, IAuditableEntity,ISoftDeleteEntity
    {
        public string NameAr { get; private set; } = null!;
        public string NameEn { get; private set; } = null!;
        public string ImageName { get; private set; } = null!;
        public string ImageFileId { get; set; } = null!;
        public bool IsActive { get; private set; }
        public Guid? CategoryId { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; private set; }

        public bool IsDeleted {  get; set; }

        public DateTime? DeletedAt {  get; set; }

        public DateTime? RestoredAt {  get; set; }

        public void setName(string nameAr, string nameEn)
        {
            NameAr = nameAr;
            NameEn = nameEn;
        }

        public void SetImage(string imageName, string imageFileId)
        {
            ImageName = imageName;
            ImageFileId = imageFileId;
        }

        public void SetCategoryId(Guid? categoryId)
        {
            CategoryId = categoryId;
        }
        public void SetActivation(bool isActive)
        {
            IsActive = isActive;
        }

        public void Restored()
        {
            IsDeleted =false;
        }
    }
}
