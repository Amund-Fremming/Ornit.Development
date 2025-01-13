namespace Ornit.Backend.src.Features.Auth0
{
    public record ClientRegistrationResponse(string AccessToken, string RefreshToken, int ExpiresIn);
}