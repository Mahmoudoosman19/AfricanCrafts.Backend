using Microsoft.AspNetCore.Mvc.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AfricanCrafts.Api.Middlewares
{
    public class ActionPermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public ActionPermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>() is { } actionDescriptor)
            {
                var permissionName = $"{actionDescriptor.ControllerName}.{actionDescriptor.ActionName}";
                context.Items["RequiredPermission"] = permissionName;
            }

            await _next(context);
        }
    }
}
