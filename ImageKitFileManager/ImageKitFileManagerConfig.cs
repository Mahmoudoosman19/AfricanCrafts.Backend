using ImageKitFileManager.Abstractions;
using ImageKitFileManager.Helpers;
using ImageKitFileManager.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImageKitFileManager
{
    public static class ImageKitFileManagerConfig
    {
        public static IServiceCollection AddImageKitFileManagerConfig(this IServiceCollection services, IConfiguration config)
        {
            var imageKitBaseUrl = config["ImageKit:Url"];

            if (imageKitBaseUrl == null)
                throw new ArgumentNullException(nameof(imageKitBaseUrl));

            ImageKitBaseUrl.SetImageKitBaseUrl(imageKitBaseUrl);
            services.AddScoped<IImageKitService, ImageKitService>();

            return services;
        }
    }
}
