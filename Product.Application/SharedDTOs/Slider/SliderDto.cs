namespace Product.Application.SharedDTOs.Slider;

public class SliderDto
{
    public Guid Id { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string? ImageUrl { get; set; }
    public Guid? CategoryId { get; set; }
}
