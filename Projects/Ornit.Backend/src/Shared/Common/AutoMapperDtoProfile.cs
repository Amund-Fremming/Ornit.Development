using AutoMapper;
using Ornit.Backend.src.Features.User;

namespace Ornit.Backend.src.Shared.Common
{
    public class AutoMapperDtoProfile : Profile
    {
        public AutoMapperDtoProfile()
        {
            CreateMap<UserEntity, UserDto>();
        }
    }
}