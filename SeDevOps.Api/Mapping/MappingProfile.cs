using AutoMapper;
using SeDevOps.Api.Dtos;
using SeDevOps.Data.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SeDevOps.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Server, ServerDto>().ReverseMap();

            CreateMap<Application, ApplicationDto>();
            CreateMap<ApplicationCreateDto, Application>();

            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<Note, NoteDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.ApplicationName, opt => opt.MapFrom(src => src.Application.Name));

            CreateMap<NoteDto, Note>();
        }
    }
}
