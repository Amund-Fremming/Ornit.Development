﻿using FeatureBasic.src.Shared.Abstractions;

namespace FeatureBasic.src.Features.User
{
    public class UserController(IUserRepository repository) : EntityControllerBase<UserEntity>(repository)
    {
        // Add your specific methods here.
    }
}