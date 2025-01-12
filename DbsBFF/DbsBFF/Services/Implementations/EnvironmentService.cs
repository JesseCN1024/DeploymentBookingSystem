using DbsBFF.Services.Interfaces;
using DbsBFF.Utilities;
using DbsEnvManagementService.Presentation.Constants;
using DbsEnvManagementService.Utilities;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace DbsBFF.Services.Implementations
{
    public class EnvironmentService : IEnvironmentService
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;

        public EnvironmentService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> EnvExistsAsync(Guid envId)
        {
            var uri = JsonUtils.GenerateUri(_configuration, "EnvironmentService", "GetEnv").Replace("{id}", envId.ToString());
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
        }
    }
}
