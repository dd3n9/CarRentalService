using System.Security.Claims;

namespace CarRentalService.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserIdClaimValue(this HttpContext context)
        {
            return GetStringClaimValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", context);
        }

        private static string GetStringClaimValue(string key, HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            return identity?.FindFirst(key)?.Value;
        }
    }
}
