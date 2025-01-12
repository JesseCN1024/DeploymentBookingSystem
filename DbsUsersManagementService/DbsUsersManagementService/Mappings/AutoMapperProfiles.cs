using AutoMapper;
using DbsUsersManagementService.Models.Domain;
using DbsUsersManagementService.Models.DTOs;

namespace DbsUsersManagementService.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RegisterRequestDto, User>().ReverseMap();
            CreateMap<UpdateResponseDto, User>().ReverseMap();
        }
    }
}
