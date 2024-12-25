using FeatureResult.src.Shared.Abstractions;
using FeatureResult.src.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace FeatureResult.src.Features.User
{
    public class UserEntity : IIdentityEntity
    {
        [Key]
        public int ID { get; set; }

        public UserRole UserRole
        {
            get => UserRole; set
            {
                UserRole = UserRole.User;
            }
        }

        public string Username { get; set; } = string.Empty;

        public UserEntity()
        { }

        public UserEntity(string username) => Username = username;
    }
}