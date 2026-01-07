using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace Product.Application.Features.Categories.Commands.AddCategory
{
    public sealed class AddCategoryCommand : ICommand
    {
        [DisplayName("الاسم العربي")]
        public string NameAr { get; init; }

        [DisplayName("الاسم باللغة الانجليزية")]
        public string NameEn { get; init; }
        public Guid? ParentId { get; init; }
        [DisplayName("صورة الفئة")]
        public IFormFile Image { get; init; }
        [DisplayName("مجموعة الحجم")]
        public Guid SizeGroupId { get; init; }
    }


}
