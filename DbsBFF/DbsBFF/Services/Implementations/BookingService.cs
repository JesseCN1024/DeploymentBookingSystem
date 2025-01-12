using DbsBFF.Models.DTOs;
using DbsBFF.Services.Interfaces;
using DbsBFF.Utilities;
using DbsEnvManagementService.Presentation.Constants;
using DbsEnvManagementService.Utilities;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace DbsBFF.Services.Implementations
{
    public class BookingService : IBookingService
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IEnvironmentService _environmentService;

        public BookingService(HttpClient httpClient, 
            IConfiguration configuration, 
            ILogger<UserService> logger, 
            IHttpContextAccessor httpContextAccessor,
            IEnvironmentService environmentService,
            IUserService userService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _environmentService = environmentService;
        }
        public async Task<Guid> CreateBookingAsync(CreatingBookingRequestDto requestDto)
        {
            // Check UserId and EnvId
            await _userService.UserExistsAsync(requestDto.UserId);
            await _environmentService.EnvExistsAsync(requestDto.EnvironmentId);

            // Start Requesting
            var uri = JsonUtils.GenerateUri(_configuration, "BookingService", "Create");
            // Requesting
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create(requestDto)
            };
            // Add token
            var token = JsonUtils.GetToken(_httpContextAccessor); // Replace with actual token
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            // Send request
            var response = await _httpClient.SendAsync(request);



            if (!response.IsSuccessStatusCode)
            {
                var result2 = await response.Content.ReadAsStringAsync();
                throw new ServiceException(ErrorCode.Conflict, result2);
            }

            // Get header
            var locationHeader = response.Headers.Location;
            if (locationHeader == null)
            {
                throw new HttpRequestException("Location header is missing in the response.");
            }
            var locationHeaderString = locationHeader.ToString();
            // Get Id from header
            var match = Regex.Match(locationHeaderString, @"\/booking\/([0-9a-fA-F-]+)");
            if (match.Success && Guid.TryParse(match.Groups[1].Value, out var guid))
            {
                return guid;
            }

            return Guid.NewGuid();
        }

        public async Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync(Guid? userId, Guid? environmentId, DateTime? fromDate, DateTime? toDate)
        {
            var uri = JsonUtils.GenerateUri(_configuration, "BookingService", "GetAll");
            // Add query parameters
            var queryParams = new List<string>();
            if (userId.HasValue)
            {
                queryParams.Add($"userId={userId}");
            }
            if (environmentId.HasValue)
            {
                queryParams.Add($"environmentId={environmentId}");
            }
            if (fromDate.HasValue)
            {
                queryParams.Add($"fromDate={fromDate.Value.ToString()}");
            }
            if (toDate.HasValue)
            {
                queryParams.Add($"toDate={toDate.Value.ToString()}");
            }
            if (queryParams.Any())
            {
                uri = $"{uri}?{string.Join("&", queryParams)}";
            }

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            // Add token
            var token = JsonUtils.GetToken(_httpContextAccessor);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            // Send request
            var response = await _httpClient.SendAsync(request);

            // Handle result 
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ServiceException(ErrorCode.Conflict, result);
            }
            var loginResponse = JsonUtils.DeserializeOrThrow<IEnumerable<BookingResponseDto>>(result);

            return loginResponse;
        }
    }
}
