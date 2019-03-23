using System;
using System.Linq;
using AutoMapper;
using WorkSupply.Core.DTOs.Auth;
using WorkSupply.Core.DTOs.Pagination;
using WorkSupply.Core.DTOs.WorkLog;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Pagination;
using WorkSupply.Core.Models.Token;
using WorkSupply.Core.Models.WorkLog;

namespace WorkSupply.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Pagination
            CreateMap<PaginatedList<WorkLog>, PaginatedListDto<WorkLogDto>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.ToList()));
            
            CreateMap<PaginatedList<ApplicationUser>, PaginatedListDto<ApplicationUserDto>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.ToList()));
            
            // Auth
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<Jwt, JwtTokenDto>();
            
            // Work log
            CreateMap<CreateWorkLogDto, WorkLog>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration.Value.Ticks));
            CreateMap<WorkLog, WorkLogDto>()
                .ForMember(dest => dest.ResolvedDate, opt => opt.MapFrom(src => src.ModifiedDate))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => new TimeSpan(src.Duration)));
            CreateMap<WorkLogGraphData, WorkLogGraphDataDto>()
                .ForMember(dest => dest.TotalHours, opt => opt.MapFrom(src => new TimeSpan(src.TotalHours)))
                .ForMember(dest => dest.TotalPending, opt => opt.MapFrom(src => new TimeSpan(src.TotalPending)))
                .ForMember(dest => dest.TotalApproved, opt => opt.MapFrom(src => new TimeSpan(src.TotalApproved)))
                .ForMember(dest => dest.TotalRejected, opt => opt.MapFrom(src => new TimeSpan(src.TotalRejected)));
        }
    }
}