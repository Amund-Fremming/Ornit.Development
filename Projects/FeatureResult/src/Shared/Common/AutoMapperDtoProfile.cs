using AutoMapper;
using FeatureResult.src.Features.Example;

namespace FeatureResult.src.Shared.Common
{
    public class AutoMapperDtoProfile : Profile
    {
        public AutoMapperDtoProfile()
        {
            CreateMap<ExampleEntity, ExampleDto>();
        }
    }
}