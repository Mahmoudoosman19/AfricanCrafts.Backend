using Microsoft.AspNetCore.Http;

namespace Common.Application.Helpers
{
    public class BaseUrl
    {
        public static string GetBaseUrl()
        {
            var httpContextAccessor = new HttpContextAccessor();
            var request = httpContextAccessor.HttpContext!.Request;
            return $"{request.Scheme}://{request.Host}{request.PathBase}/";
        }
    }
}
