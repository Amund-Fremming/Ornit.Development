using AutoMapper;
using FeatureBasic.src.Features.User;

namespace FeatureBasic.src.Shared.Common
{
    public class AutoMapperDtoProfile : Profile
    {
        public AutoMapperDtoProfile()
        {
            CreateMap<UserEntity, UserDto>();
        }
    }
}