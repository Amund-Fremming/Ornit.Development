using AutoMapper;
using Ornit.Backend.src.Features.User;

namespace Ornit.Backend.src.Shared.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserEntity, UserDto>();
        }
    }
}