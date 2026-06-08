using AutoMapper;
using SeDevOps.Api.Dtos;
using SeDevOps.Data.Entities;

namespace SeDevOps.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Application
        CreateMap<Application, ApplicationDto>().ReverseMap();

        // Server
        CreateMap<Server, ServerDto>().ReverseMap();

        // User
        CreateMap<User, UserDto>().ReverseMap();

        // Role
        CreateMap<Role, RoleDto>().ReverseMap();

        // UserRole
        CreateMap<UserRole, UserRoleDto>().ReverseMap();

        // Note
        CreateMap<Note, NoteDto>().ReverseMap();
    }
}