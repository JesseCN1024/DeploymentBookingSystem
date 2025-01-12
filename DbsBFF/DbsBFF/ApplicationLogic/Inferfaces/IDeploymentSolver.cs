using DbsBFF.Models.DTOs;

namespace DbsBFF.ApplicationLogic.Inferfaces
{
    public interface IDeploymentSolver
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto requesetDto);
        Task<Guid> CreateBookingAsync(CreatingBookingRequestDto requestDto);
        Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync(Guid? userId, Guid? environmentId, DateTime? fromDate, DateTime? toDate);

    }
}
