using DbsBFF.ApplicationLogic.Inferfaces;
using DbsBFF.Models.DTOs;
using DbsBFF.Services.Interfaces;

namespace DbsBFF.ApplicationLogic.Implementations
{
    public class DeploymentSolver : IDeploymentSolver
    {
        private readonly IUserService _userService;
        private readonly IBookingService _bookingService;

        public DeploymentSolver(IUserService userService, IBookingService bookingService)
        {
            _userService = userService;
            _bookingService = bookingService;
        }

        public async Task<Guid> CreateBookingAsync(CreatingBookingRequestDto requestDto)
        {
            try
            {
                return await _bookingService.CreateBookingAsync(requestDto);
            }
            catch(HttpRequestException ex)
            {
                throw new ApplicationException("Failed to connect to the user service.", ex);
            }
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto requesetDto)
        {
            try
            {
                return await _userService.LoginAsync(requesetDto);
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to connect to the user service.", ex);
            }
        }

        public async Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync(Guid? userId, Guid? environmentId, DateTime? fromDate, DateTime? toDate)
        {

            try
            {
                return await _bookingService.GetAllBookingsAsync(userId, environmentId, fromDate, toDate);
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to connect to the booking service.", ex);
            }
        }
    }
}
