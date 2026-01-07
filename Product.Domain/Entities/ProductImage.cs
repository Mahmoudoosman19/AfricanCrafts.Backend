using Common.Domain.Primitives;
using ImageKitFileManager.Abstractions;
using ImageKitFileManager.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductImage : Entity<Guid>
    {
        public string ImageName { get; private set; } = null!;
        public string ImageFileId { get; set; } = null!;
        public Guid ProductId { get; private set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; private set; }

        public string ColorCode { get; private set; } = null!;

        public void SetImage(IImageKitService _imageKitService, IFormFile ImageFile, Guid folderId)
        {
            var result = _imageKitService.UploadFileAsync(ImageFile, FileType.Product, folderId).GetAwaiter().GetResult();
            ImageFileId = result.FileId;
            ImageName = result.Name;
        }

        public void SetProduct(Guid productId)
        {
            ProductId = productId;
        }

        public void SetColor(string colorCode)
        {
            ColorCode = colorCode;
        }
    }
}
