namespace FeatureResult.src.Features.Auth
{
    public record ClientRegistrationResponse(string AccessToken, string RefreshToken, int ExpiresIn);
}