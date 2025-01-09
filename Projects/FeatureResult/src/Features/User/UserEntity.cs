using FeatureResult.src.Shared.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace FeatureResult.src.Features.User
{
    public class UserEntity : IIdentityEntity
    {
        [Key]
        public int Id { get; set; }

        public string Auth0Id { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public UserEntity()
        { }
    }
}