using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace Product.Application.Features.Categories.Commands.UpdateCategory
{
    public sealed class UpdateCategoryCommand : ICommand
    {
        [DisplayName("رقم التعريف")]
        public Guid Id { get; set; }
        [DisplayName("الاسم العربي")]
        public string NameAr { get; init; }

        [DisplayName("الاسم باللغة الانجليزية")]
        public string NameEn { get; init; }
        public Guid? ParentId { get; init; }
        public IFormFile? Image { get; init; }
        [DisplayName("مجموعة الحجم")]
        public Guid SizeGroupId { get; init; }
    }
}
