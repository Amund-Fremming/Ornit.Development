using Ornit.Backend.src.Shared.Abstractions;

namespace Ornit.Backend.src.Features.User
{
    public class UserDto : ITypeScriptModel
    {
        public int Id { get; set; }
        public string Auth0Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}