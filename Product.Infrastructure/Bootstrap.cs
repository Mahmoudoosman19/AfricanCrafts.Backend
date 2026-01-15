using Common.Application.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Abstractions;
using Product.Infrastructure.Service;
using Product.Infrastructure.Services;
using System.Globalization;

namespace Product.Infrastructure;

public static class Bootstrap
{
    public static IServiceCollection AddInfrastructureStrapping(this IServiceCollection services)
    {
        services.AddLocalization();
        services.AddSingleton<ITimeService, TimeService>();

      
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

        services.AddBadRequestObjectResult();

        return services;
    }
}
