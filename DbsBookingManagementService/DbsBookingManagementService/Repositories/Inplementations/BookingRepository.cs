using DbsBookingManagementService.Data;
using DbsBookingManagementService.Models.Domain;
using DbsBookingManagementService.Repositories.Interfaces;
using DbsBookingManagementService.Presentation.Constants;
using DbsBookingManagementService.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DbsBookingManagementService.Repositories.Inplementations
{
    public class BookingRepository : IBookingRepository
    {
        private AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckBookingOverlapping(Guid envId, DateTime startDateTime, DateTime endDateTime, Guid? avoidBookingId)
        {
            try
            {
                var query = _context.Bookings.AsQueryable();
                query = query.Where(b => b.EnvironmentId == envId && !b.IsDeleted);
                query = query.Where(b => b.StartDateTime < endDateTime && b.EndDateTime > startDateTime);
                // Avoid checking the current existing bookingId
                if (avoidBookingId.HasValue)
                {
                    query = query.Where(b => b.Id != avoidBookingId.Value);
                }

                if (await query.AnyAsync())
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(ErrorCode.InternalServerError, e.Message);
            }
         }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            await _context.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;

        }

        
        public async Task<Booking?> GetBookingByIdAsync(Guid id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync(Guid? userId, Guid? environmentId, DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Bookings.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(b => b.UserId == userId.Value);
            }

            if (environmentId.HasValue)
            {
                query = query.Where(b => b.EnvironmentId == environmentId.Value);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(b => b.StartDateTime >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(b => b.EndDateTime <= toDate.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByEnvIdAsync(Guid envId)
        {
            var query = _context.Bookings.AsQueryable();
            query = query.Where(b => b.EnvironmentId == envId && !b.IsDeleted);
            return await query.ToListAsync();
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }
    }
}
