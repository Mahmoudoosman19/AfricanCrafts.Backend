//using Bus.Contracts.Enum;
//using Bus.Contracts.Models.Notifications;
using Common.Domain.Primitives;
using Product.Domain.DomainEvents;
using Product.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class Product : AggregateRoot<Guid>, IAuditableEntity
    {
        private List<ProductImage> _productImages = [];
        private List<ProductExtension> _productExtensions = [];
        private List<ProductComment> _productComments = [];

        public string NameAr { get; private set; } = null!;
        public string NameEn { get; private set; } = null!;
        public string DescriptionAr { get; private set; } = null!;
        public string DescriptionEn { get; private set; } = null!;
        public decimal Price { get; private set; }
        public decimal DiscountPrice { get; private set; } = 0;
        public Guid CategoryId { get; private set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; private set; }
        public bool IsActive { get; private set; } = false;
        public Guid CreatedBy { get; private set; }
        public Guid VendorId { get; private set; }
        public Guid? PointsId { get; private set; }
        [ForeignKey(nameof(PointsId))]
        public virtual Point? Points { get; private set; }

        public ProductStatus Status { get; private set; } = ProductStatus.InReview;
        public ICollection<ProductImage> Images => _productImages;
        public ICollection<ProductExtension> Extensions => _productExtensions;
        public ICollection<ProductComment> Comments => _productComments;
        public Guid ImagesFolderName { get; private set; }
        [Range(0.0, 5.0)]
        public double Rate { get; private set; } = 2.5;
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public string ProductCode { get; private set; } = null!;
        public void SetName(string nameAr, string nameEn)
        {
            NameAr = nameAr;
            NameEn = nameEn;
        }

        public void SetDescription(string descriptionAr, string descriptionEn)
        {
            DescriptionAr = descriptionAr;
            DescriptionEn = descriptionEn;
        }

        public void SetPrice(decimal price)
        {
            Price = price;
        }

        public void SetStatus(ProductStatus status)
        {
            Status = status;
        }

        public void SetCategory(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        public void SetActivation(bool isActive)
        {
            IsActive = isActive;
            RaiseDomainEvent(new ProductToggleActivationDomainEvent(Guid.NewGuid(), Id, IsActive));
        }

        public void UpdateEvent()
        {
            if (IsActive && Status == ProductStatus.Approved)
            {
                RaiseDomainEvent(new ProductToggleActivationDomainEvent(Guid.NewGuid(), Id, false));
                RaiseDomainEvent(new ProductToggleActivationDomainEvent(Guid.NewGuid(), Id, true));
            }
        }

        public void SetCreatorAndOwnerInfo(Guid createdBy, Guid vendorId)
        {
            CreatedBy = createdBy;
            VendorId = vendorId;
        }

        public void SetPoints(Guid? pointsId)
        {
            PointsId = pointsId;
        }

        public void SetImagesFolderName(Guid name)
        {
            ImagesFolderName = name;
        }
        public void SetRate(double rate)
        {
            Rate = rate;
        }
        public void AddImage(ProductImage image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            _productImages.Add(image);
        }
        public void AddRangeImage(List<ProductImage> images)
        {
            if (images == null) throw new ArgumentNullException(nameof(images));
            _productImages.AddRange(images);
        }

        public void AddComment(ProductComment comment)
        {
            _productComments.Add(comment);
        }

        public void AddRangeComments(List<ProductComment> comments)
        {
            _productComments.AddRange(comments);
        }

        public void RemoveImage(ProductImage image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            _productImages.Remove(image);
        }

        public void UpdateImages(List<ProductImage> images)
        {
            if (images == null) throw new ArgumentNullException(nameof(images));
            _productImages.Clear();
            _productImages.AddRange(images);
        }

        public void AddExtension(ProductExtension extension)
        {
            if (extension == null) throw new ArgumentNullException(nameof(extension));
            _productExtensions.Add(extension);
        }
        public void AddRangeExtension(List<ProductExtension> extensions)
        {
            if (extensions == null) throw new ArgumentNullException(nameof(extensions));
            _productExtensions.AddRange(extensions);
        }

        public void RemoveExtension(ProductExtension extension)
        {
            if (extension == null) throw new ArgumentNullException(nameof(extension));
            _productExtensions.Remove(extension);
        }

        public void UpdateExtensions(List<ProductExtension> extensions)
        {
            if (extensions == null) throw new ArgumentNullException(nameof(extensions));
            _productExtensions.Clear();
            _productExtensions.AddRange(extensions);
        }

        public void SetCode(string productCode)
        {
            this.ProductCode = productCode;
        }
        //public void CreateNotification(NotificationsModel notifications)
        //{
        //    RaiseDomainEvent(new CreateProductNotificationsDomainEvent(Guid.NewGuid(), notifications));
        //}
    }
}
