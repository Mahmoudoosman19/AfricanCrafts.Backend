using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Abstraction;
using Order.Infrastructure.Seeder;
using Order.Infrastructure.Services;
using System.Globalization;

namespace Order.Infrastructure;

public static class Bootstrap
{
    public static async Task<IServiceCollection> AddInfrastructureStrapping(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddLocalization();

        // external sevice config
        var externalServicesSection = configuration.GetSection("ExternalServices");
        var productApiUrl = externalServicesSection["baseUrl"];
        if (string.IsNullOrEmpty(productApiUrl))
        {
            throw new InvalidOperationException("Product API URL is not configured in appsettings.json");
        }

        services.AddHttpClient<IProductService, ProductService>(client =>
        {
            client.BaseAddress = new Uri(productApiUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("ar")
            };

            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.RequestCultureProviders = new List<IRequestCultureProvider>
            {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider(),
                    new AcceptLanguageHeaderRequestCultureProvider()
            };
        });
        await services.AddDBSeederExtension();
       
        return services;
    }
}
