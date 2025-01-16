using AutoMapper;
using Ornit.Backend.src.Features.Auth0;
using Ornit.Backend.src.Features.User;

namespace Ornit.Backend.src.Shared.Mappings
{
    public class DomainToContractProfile : Profile
    {
        public DomainToContractProfile()
        {
            CreateMap<Auth0RegisterResponse, UserEntity>()
                .ForMember(dest => dest.Auth0Id, opt => opt.MapFrom(src => src.Id));

        }
    }
}