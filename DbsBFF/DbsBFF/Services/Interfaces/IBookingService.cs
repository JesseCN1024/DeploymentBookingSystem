using DbsBFF.Models.DTOs;

namespace DbsBFF.Services.Interfaces
{
    public interface IBookingService
    {
        Task<Guid> CreateBookingAsync(CreatingBookingRequestDto requestDto);
        Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync(Guid? userId, Guid? environmentId, DateTime? fromDate, DateTime? toDate);
    }
}
