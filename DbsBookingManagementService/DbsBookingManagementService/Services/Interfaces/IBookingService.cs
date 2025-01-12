using DbsBookingManagementService.Models.Domain;
using DbsBookingManagementService.Models.DTOs;

namespace DbsBookingManagementService.Services.Interfaces
{
    public interface IBookingService
    {
        Task<Guid> CreateBookingAsync(CreatingBookingRequestDto request);
        Task<BookingResponseDto?> UpdateBookingAsync(Guid bookingId, EditBookingRequestDto request);
        Task DeleteBookingAsync(Guid bookingId);
        Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync(Guid? userId, Guid? environmentId, DateTime? fromDate, DateTime? toDate);
        
        Task<BookingResponseDto?> GetBookingByIdAsync(Guid bookingId);
        


    }
}
