using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Product.Domain.Entities;

namespace AfricanCrafts.Api.Helpers
{
    public class DynamicActionPermissionRequirement : IAuthorizationRequirement { }

    public class DynamicActionPermissionHandler : AuthorizationHandler<DynamicActionPermissionRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DynamicActionPermissionHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            DynamicActionPermissionRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return Task.CompletedTask;

            var requiredPermission = httpContext.Items["RequiredPermission"] as string;

            if (string.IsNullOrEmpty(requiredPermission))
            {
                return Task.CompletedTask; 
            }

            if (context.User.HasClaim(c => c.Type == "Permissions" && c.Value == "Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (context.User.HasClaim("Permissions", requiredPermission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
