using System.Security.Claims;

namespace FeatureResult.src.Shared.Common
{
    public static class TokenDecoder
    {
        // TODO : Need update
        public static string? GetUserId(ClaimsPrincipal user) => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}