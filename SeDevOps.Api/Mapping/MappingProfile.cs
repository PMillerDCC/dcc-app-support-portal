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
            CreateMap<Application, ApplicationDto>().ReverseMap();
            CreateMap<Note, NoteDto>().ReverseMap();
        }
    }
}
