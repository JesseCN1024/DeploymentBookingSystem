using AutoMapper;
using DbsEnvManagementService.Models.Domain;
using DbsEnvManagementService.Models.DTOs;

namespace DbsEnvManagementService.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Env, EnvResponseDto>().ReverseMap();
        }
    }
}
