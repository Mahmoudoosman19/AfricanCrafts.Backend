using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class Size : Entity<Guid>, IAuditableEntity
    {
        public string NameAr { get; private set; } = null!;
        public string NameEn { get; private set; } = null!;
        public string? DescriptionAr { get; private set; }
        public string? DescriptionEn { get; private set; }
        public Guid SizeGroupId { get; private set; }
        [ForeignKey(nameof(SizeGroupId))]
        public virtual SizeGroup? SizeGroup { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsActive { get; set; }

        public static Size Create(
            string nameAr,
            string nameEn,
            string? descriptionAr,
            string? descriptionEn,
            Guid sizeGroupId,
            bool isActive = true)
        {
            return new Size
            {
                NameAr = nameAr,
                NameEn = nameEn,
                DescriptionAr = descriptionAr,
                DescriptionEn = descriptionEn,
                SizeGroupId = sizeGroupId,
                IsActive = isActive
            };
        }

        public void SetName(string nameAr, string nameEn)
        {
            NameAr = nameAr;
            NameEn = nameEn;
        }

        public void SetDescription(string? descriptionAr, string? descriptionEn)
        {
            DescriptionAr = descriptionAr;
            DescriptionEn = descriptionEn;
        }

        public void SetSizeGroup(Guid sizeGroupId)
        {
            SizeGroupId = sizeGroupId;
        }
        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }

    }
}
