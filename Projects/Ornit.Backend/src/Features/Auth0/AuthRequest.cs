using Ornit.Backend.src.Shared.Abstractions;

namespace Ornit.Backend.src.Features.Auth0
{
    public record AuthRequest(string Email, string Password) : ITypeScriptModel;
}