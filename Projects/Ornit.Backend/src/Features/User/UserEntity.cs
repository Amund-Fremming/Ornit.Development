﻿using Ornit.Backend.src.Shared.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Ornit.Backend.src.Features.User
{
    public class UserEntity : IIdentity, ITypeScriptModel
    {
        [Key]
        public int Id { get; set; }

        public string Auth0Id { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public UserEntity()
        { }

        public UserEntity(string auth0Id, string email)
        {
            Auth0Id = auth0Id;
            this.Email = email;
        }
    }
}