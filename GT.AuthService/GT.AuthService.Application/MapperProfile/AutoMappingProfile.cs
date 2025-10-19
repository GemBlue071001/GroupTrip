using AutoMapper;
using GT.AuthService.Domain.Entities;
using GT.AuthService.Domain.Models.Authen;
using GT.AuthService.Domain.Models.Role;

namespace GT.AuthService.Application.MapperProfile
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<ApplicationRole,CreateRoleModel>().ReverseMap();
            CreateMap<ApplicationRole,UpdateRoleModel>().ReverseMap();
            CreateMap<ApplicationRole,ResponseRoleModel>().ReverseMap();

            CreateMap<ApplicationUser, ResponseUserModel>().ReverseMap();
        }
    }
}
