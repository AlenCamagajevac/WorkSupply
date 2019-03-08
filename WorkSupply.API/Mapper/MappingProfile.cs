using AutoMapper;
using WorkSupply.Core.DTOs.Auth;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Token;

namespace WorkSupply.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Auth
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Jwt, JwtTokenDto>();
        }
    }
}