using Microsoft.AspNetCore.Http;

namespace Product.Application.Features.Advertisements.Command.AddAdvertisement
{
    public class AddAdvertisementCommand : ICommand
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public string AdvertisementUrl { get;  set; }
    }
}
