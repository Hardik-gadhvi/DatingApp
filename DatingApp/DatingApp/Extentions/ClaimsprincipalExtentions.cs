using System.Security.Claims;

namespace DatingApp.Extentions
{
    public static class ClaimsprincipalExtentions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
