using System.Security.Claims;

namespace FeatureResult.src.Shared.Common
{
    public static class TokenExtractor
    {
        public static string? GetUserId(ClaimsPrincipal user) => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        public static string? GetAuth0UserId(ClaimsPrincipal user) => GetUserId(user)?.Replace("auth0|", "");
    }
}