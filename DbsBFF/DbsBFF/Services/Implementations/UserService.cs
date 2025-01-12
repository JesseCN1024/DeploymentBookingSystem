using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DbsBFF.Models.DTOs;
using DbsBFF.Services.Interfaces;
using DbsBFF.Utilities;
using DbsEnvManagementService.Presentation.Constants;
using DbsEnvManagementService.Utilities;

namespace DbsBFF.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(HttpClient httpClient, IConfiguration configuration, ILogger<UserService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto requestDto)
        {
            var uri = JsonUtils.GenerateUri(_configuration, "UserService", "Login");
            
            var response = await _httpClient.PostAsJsonAsync(uri, requestDto);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to login: {response.ReasonPhrase}");
            }

            var result = await response.Content.ReadAsStringAsync();

            _logger.LogInformation($"Response Content: {result}");

            var loginResponse = JsonUtils.DeserializeOrThrow<LoginResponseDto>(result);

            return loginResponse;
        }

        public async Task<bool> UserExistsAsync(Guid userId)
        {
            var uri = JsonUtils.GenerateUri(_configuration, "UserService", "GetUser").Replace("{id}", userId.ToString());
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            // Add Token to request
            var token = JsonUtils.GetToken(_httpContextAccessor); // Replace with actual token
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            // Send request
            var response = await _httpClient.SendAsync(request);
            // Check service exception
            if (!response.IsSuccessStatusCode)
            {
                var result2 = await response.Content.ReadAsStringAsync();
                throw new ServiceException(ErrorCode.Conflict, result2);
            }


            return true;

            //var result = await response.Content.ReadAsStringAsync();
            //return bool.Parse(result);
        }





    }

}
