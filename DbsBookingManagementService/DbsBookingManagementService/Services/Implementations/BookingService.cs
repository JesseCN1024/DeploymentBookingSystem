using AutoMapper;
using DbsBookingManagementService.Models.Domain;
using DbsBookingManagementService.Models.DTOs;
using DbsBookingManagementService.Repositories.Interfaces;
using DbsBookingManagementService.Services.Interfaces;
using DbsBookingManagementService.Presentation.Constants;
using DbsBookingManagementService.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DbsBookingManagementService.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private IHttpContextAccessor _httpContextAssessor;
        private IConfiguration _configuration;
        private IBookingRepository _repository;
        private IMapper _mapper;

        public BookingService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IBookingRepository repository, IMapper mapper)
        {
            _httpContextAssessor = httpContextAccessor;
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync(Guid? userId, Guid? environmentId, DateTime? fromDate, DateTime? toDate)
        {
            var bookings = await _repository.GetAllBookingsAsync(userId, environmentId, fromDate, toDate);
            return _mapper.Map<IEnumerable<BookingResponseDto>>(bookings);
        }

        public async Task<Guid> CreateBookingAsync(CreatingBookingRequestDto request)
        {
            var userId = GetUserIdAndRoleFromClaims().UserId; // user currently logged in
            // Checking overlapping
            await CheckBookingOverlapping(request.EnvironmentId, request.StartDateTime, request.EndDateTime);

            // Check UserId and EnvironementId in BFF already 

            var booking = new Booking
            {
                UserId = request.UserId,
                EnvironmentId = request.EnvironmentId,
                StartDateTime = request.StartDateTime,
                EndDateTime = request.EndDateTime,
                Notes = request.Notes,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = userId,
                UpdatedDate = DateTime.UtcNow
            };
            try
            {
                var result = await _repository.CreateBookingAsync(booking);
                return result.Id;
            }
            catch(Exception ex)
            {
                throw new UserFriendlyException(ErrorCode.ServiceUnavailable, ex.Message);
            }
        }


        public async Task DeleteBookingAsync(Guid bookingId)
        {
            // check booking exists
            var existingBooking = await GetExistingBooking(bookingId);
            // User must be admin or owner
            var (userId, role) = GetUserIdAndRoleFromClaims();
            if (role != Constants.Roles.Admin && existingBooking!=null && existingBooking.UserId != userId)
            {
                throw new UserFriendlyException(ErrorCode.Forbidden, "You are not authorized to delete this booking.");
            }


            if (existingBooking != null)
            {
                existingBooking.IsDeleted = true;
                existingBooking.UpdatedDate = DateTime.UtcNow;
                existingBooking.UpdatedBy = userId;
                await _repository.UpdateBookingAsync(existingBooking);
            }

        }

        public async Task<BookingResponseDto?> GetBookingByIdAsync(Guid bookingId)
        {
            var booking = await GetExistingBooking(bookingId);
            return _mapper.Map<BookingResponseDto>(booking);
        }

        public async Task<BookingResponseDto?> UpdateBookingAsync(Guid bookingId, EditBookingRequestDto request)
        {
            // Check Booking ID
            var existingBooking = await GetExistingBooking(bookingId);
            if (existingBooking != null)
            {
                // Check request overlapping
                await CheckBookingOverlapping(existingBooking.EnvironmentId, request.StartDateTime, request.EndDateTime, existingBooking.Id);

                // Done validating
                existingBooking.StartDateTime = request.StartDateTime;
                existingBooking.EndDateTime = request.EndDateTime;
                existingBooking.Notes = request.Notes;
                existingBooking.UpdatedDate = DateTime.UtcNow;
                existingBooking.UpdatedBy = GetUserIdAndRoleFromClaims().UserId ;
                await _repository.UpdateBookingAsync(existingBooking);
                return _mapper.Map<BookingResponseDto>(existingBooking);
            }
            return null;
        }









        // Additional Methods
        private async Task<Booking?> GetExistingBooking(Guid bookingId, bool isThrownException = true)
        {
            var booking = await _repository.GetBookingByIdAsync(bookingId);
            if (booking == null && isThrownException)
            {
                throw new UserFriendlyException(ErrorCode.NotFound, $"Booking with '${bookingId}' is not found.");
            }
            return booking;
        }
        private async Task CheckBookingOverlapping(Guid envId, DateTime startDateTime, DateTime endDateTime, Guid? avoidBookingId=null)
        {
            var isOverlapping = await _repository.CheckBookingOverlapping(envId, startDateTime, endDateTime, avoidBookingId);
            if (isOverlapping)
            {
                throw new UserFriendlyException(ErrorCode.BadRequest, "Booking is overlapping with another booking.");
            }

        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration["Jwt:Secret"];
            if (string.IsNullOrEmpty(secret))
            {
                throw new InvalidOperationException("JWT Secret is not configured.");
            }
            var key = Encoding.ASCII.GetBytes(secret);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return principal;
            }
            catch
            {
                throw new UserFriendlyException(ErrorCode.Unauthorized, "Invalid token.");
            }
        }

        //private Guid GetUserIdFromClaims()
        //{
        //    var token = _httpContextAssessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        throw new UserFriendlyException(ErrorCode.Unauthorized, "Token is missing.");
        //    }

        //    var principal = ValidateToken(token);
        //    var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
        //    if (userIdClaim == null)
        //    {
        //        throw new UserFriendlyException(ErrorCode.Unauthorized, "User is not authenticated.");
        //    }

        //    return Guid.Parse(userIdClaim.Value);
        //}
        private (Guid UserId, string UserRole) GetUserIdAndRoleFromClaims()
        {
            var token = _httpContextAssessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                throw new UserFriendlyException(ErrorCode.Unauthorized, "Token is missing.");
            }

            var principal = ValidateToken(token);
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            var userRoleClaim = principal.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null)
            {
                throw new UserFriendlyException(ErrorCode.Unauthorized, "User is not authenticated.");
            }

            var userId = Guid.Parse(userIdClaim.Value);
            var userRole = userRoleClaim?.Value ?? string.Empty;

            return (userId, userRole);
        }


    }
}
