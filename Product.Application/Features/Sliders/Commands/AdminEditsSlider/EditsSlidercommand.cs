using Microsoft.AspNetCore.Http;

namespace Product.Application.Features.Sliders.Commands.AdminEditsSlider
{
    public class EditsSlidercommand:ICommand
    {
        public Guid Id { get; set; }        
        public string NameAr { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public IFormFile? Image { get; set; } = null!;
        public Guid? CategoryId { get; set; }
    }
}
