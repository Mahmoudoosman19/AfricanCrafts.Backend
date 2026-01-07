using Microsoft.AspNetCore.Http;

namespace Product.Application.Features.Sliders.Commands.AddSlider;

public class AddSliderCommand : ICommand
{
    public string NameAr { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public IFormFile Image { get; set; } = null!;
    public Guid? CategoryId { get; set; }
}
