using FeatureBasic.src.Shared.Abstractions;
using FeatureBasic.src.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace FeatureBasic.src.Features.User
{
    public class UserEntity : IIdentityEntity
    {
        [Key]
        public int ID { get; set; }

        public UserRole UserRole { get => this.UserRole; set => this.UserRole = UserRole.User; }

        public string Username { get; set; } = string.Empty;

        public UserEntity()
        { }

        public UserEntity(string username) => Username = username;
    }
}