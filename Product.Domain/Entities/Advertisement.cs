using Common.Domain.Primitives;

namespace Product.Domain.Entities
{
    public class Advertisement : Entity<Guid>, IAuditableEntity
    {
        public string NameAr { get; private set; } = null!;
        public string NameEn { get; private set; } = null!;
        public string ImageName { get; private set; } = null!;
        public string ImageFileId { get; set; } = null!;
        public string DescriptionAr { get; private set; } = null!;
        public string DescriptionEn { get; private set; } = null!;
        public bool IsActive { get; private set; } = false;
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public string AdvertisementUrl { get; private set; } = null!;

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

        public void SetDescription(string descriptionAr, string descriptionEn)
        {
            DescriptionAr = descriptionAr;
            DescriptionEn = descriptionEn;
        }

        public void SetActivation(bool isActive)
        {
            IsActive = isActive;
        }

        public void SetAdvertisementUrl(string url)
        {
            AdvertisementUrl = url;
        }
    }
}






