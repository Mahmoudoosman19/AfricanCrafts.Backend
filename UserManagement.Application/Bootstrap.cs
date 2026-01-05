using Common.Application.Behaviors;
using Common.Application.Localization;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Features.Auth.Commands.ExternalLogin.Types;
using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
using UserManagement.Application.Features.Auth.Commands.Register.Type;
using UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Abstract;
using UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Type;

namespace UserManagement.Application;

public static class Bootstrap
{
    public static IServiceCollection AddApplicationStrapping(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);

            cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));

            cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(
            AssemblyReference.Assembly,
            includeInternalTypes: true);

        services.AddMapsterConfig();

        services.AddScoped<ILocalizer, Localizer>();

        services.AddScoped<BaseRegister, AdminRegisterType>();
        services.AddScoped<BaseRegister, CustomerRegisterType>();
        services.AddScoped<BaseRegister, SupervisorRegisterType>();
        services.AddScoped<BaseConfirmOTP, ConfirmUserEmailType>();
        services.AddScoped<BaseConfirmOTP, ConfirmUserPhoneType>();
        services.AddScoped<BaseConfirmOTP, ConfirmForgotPasswordType>();

        services.RegisterExternalLoginTypes();

        return services;
    }

    private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Scan(AssemblyReference.Assembly);

        services.AddSingleton(config);

        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
