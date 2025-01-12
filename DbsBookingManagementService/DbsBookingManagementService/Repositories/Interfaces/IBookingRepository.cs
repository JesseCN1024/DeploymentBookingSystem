using DbsBookingManagementService.Models.Domain;

namespace DbsBookingManagementService.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> GetBookingByIdAsync(Guid id);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task<List<Booking>> GetBookingsByEnvIdAsync(Guid envId);

        Task<bool> CheckBookingOverlapping(Guid envId, DateTime startDateTime, DateTime endDateTime, Guid? avoidBookingId);
        Task<IEnumerable<Booking>> GetAllBookingsAsync(Guid? userId, Guid? environmentId, DateTime? fromDate, DateTime? toDate);

    }
}
