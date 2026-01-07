using System.ComponentModel;

namespace Product.Application.Features.Sliders.Commands.DeleteSlider
{
    public class DeleteSliderCommand :ICommand
    {
        [DisplayName("رقم التعريف")]
        public Guid Id { get; set; }
    }
}
