using AutoMapper;
using DbsBookingManagementService.Models.Domain;
using DbsBookingManagementService.Models.DTOs;

namespace DbsBookingManagementService.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Booking, BookingResponseDto>()
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BookingId));


        }
    }
}
