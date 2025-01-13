using Ornit.Backend.src.Shared.ResultPattern;

namespace Ornit.Backend.src.Features.Auth0
{
    public interface IAuthService
    {
        Task<Result<string>> RefreshAccessToken(RefreshTokenRequest request);

        Task<Result<string>> Register(RegistrationRequest request);
    }
}