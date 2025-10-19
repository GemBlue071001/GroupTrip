

using AutoMapper;
using GT.AdminService.Domain.Entities;
using GT.AdminService.Domain.Models;
using GT.AdminService.Domain.Models.Policy;
using GT.AdminService.Domain.Models.PolicyType;

namespace GT.AdminService.Application.MapperProfile
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<ResponsePolicyType, PolicyType>().ReverseMap();
            CreateMap<UpdatePolicyType, PolicyType>().ReverseMap();
            CreateMap<CreatePolicyType, PolicyType>().ReverseMap();

            CreateMap<ResponsePolicyModel, Policy>().ReverseMap();
            CreateMap<UpdatePolicyModel, Policy>().ReverseMap();
            CreateMap<CreatePolicyModel, Policy>().ReverseMap();
        }
    }
}
