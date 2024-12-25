using AutoMapper;
using FeatureResult.src.Features.User;

namespace FeatureResult.src.Shared.Common
{
    public class AutoMapperDtoProfile : Profile
    {
        public AutoMapperDtoProfile()
        {
            CreateMap<UserEntity, UserDto>();
        }
    }
}