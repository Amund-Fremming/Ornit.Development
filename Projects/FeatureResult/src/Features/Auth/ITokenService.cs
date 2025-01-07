using NucleusResults.Core;

namespace FeatureResult.src.Features.Auth
{
    public interface ITokenService
    {
        public Result<string> GenerateToken(string Id, string email);
    }
}