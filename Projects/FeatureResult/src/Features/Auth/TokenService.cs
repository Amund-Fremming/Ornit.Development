using Microsoft.IdentityModel.Tokens;
using NucleusResults.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FeatureResult.src.Features.Auth
{
    public class TokenService(ILogger<TokenService> logger, IConfiguration configuration) : ITokenService
    {
        public Result<string> GenerateToken(string Id, string email)
        {
            try
            {
                var configurationKey = configuration.GetValue<string>("Jwt:Key");
                if (configurationKey == null)
                {
                    return new Error("Jwt key not present.");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configurationKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                    [
                        new Claim(ClaimTypes.NameIdentifier, Id),
                        new Claim(ClaimTypes.Email, email)
                    ]),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = configuration.GetValue<string>("Jwt:Issuer") ?? ("Jwt Issuer not present."),
                    Audience = configuration.GetValue<string>("Jwt:Audience") ?? throw new KeyNotFoundException("Jwt Audience not present.")
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                logger.LogError(e, "GenerateToken");
                throw;
            }
        }
    }
}