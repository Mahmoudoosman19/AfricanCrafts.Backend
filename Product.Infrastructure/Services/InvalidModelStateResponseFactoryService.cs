using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Resources;
using System.ComponentModel;
using System.Reflection;

namespace Product.Infrastructure.Services
{
    public static class InvalidModelStateResponseFactoryService
    {
        public static IServiceCollection AddBadRequestObjectResult(this IServiceCollection services)
        {


            services.AddControllers()
               .ConfigureApiBehaviorOptions(options =>
               {
                   options.InvalidModelStateResponseFactory = context =>
                   {
                       var errors = context.ModelState
                                      .Where(e => e.Value!.Errors.Count > 0)
                                      .Select(e => new
                                      {
                                          Field = e.Key,
                                          Errors = e.Value!.Errors.Select(err => new
                                          {
                                              ErrorMessage = Messages.EmptyBadRequest + " " + GetDisplayName(context.ActionDescriptor.Parameters[0].ParameterType, e.Key, services)
                                          }).ToList()
                                      }).ToList();

                       var result = new BadRequestObjectResult(new { errors });
                       return result;
                   };
               });
            return services;
        }
        private static string GetDisplayName(Type modelType, string propertyName, IServiceCollection services)
        {
            var property = modelType.GetProperty(propertyName);
            if (property != null)
            {
                var serviceProvider = services.BuildServiceProvider();

                var httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;

                string acceptLanguage = httpContext!.Request.Headers["Accept-Language"]!;
                if (acceptLanguage != null && acceptLanguage == "en")
                {
                    return propertyName;
                }
                var displayNameAttribute = property.GetCustomAttribute<DisplayNameAttribute>();
                if (displayNameAttribute != null)
                {
                    return displayNameAttribute.DisplayName;
                }
            }
            return propertyName;
        }
    }
}
