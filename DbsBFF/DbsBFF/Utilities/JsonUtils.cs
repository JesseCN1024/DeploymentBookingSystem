using DbsEnvManagementService.Presentation.Constants;
using DbsEnvManagementService.Utilities;
using System.Text.Json;

namespace DbsBFF.Utilities
{
    public static class JsonUtils
    {
        
        public static T DeserializeOrThrow<T>(string json)
        {
            try
            {
                var res = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (res == null)
                {
                    throw new UserFriendlyException(ErrorCode.Conflict, "Failed to deserialize JSON response.");
                };
                return res;
            }
            catch (JsonException ex)
            {
                throw new UserFriendlyException(ErrorCode.Conflict, "Failed to deserialize JSON response.");
            }
        }
        public static string GenerateUri(IConfiguration configuration, string serviceName, string endpointName)
        {
            var baseUrl = configuration[$"ServiceEndpoints:{serviceName}:BaseUrl"];
            var endpoint = configuration[$"ServiceEndpoints:{serviceName}:Endpoints:{endpointName}"];

            if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(endpoint))
            {
                throw new InvalidOperationException($"Configuration for service '{serviceName}' and endpoint '{endpointName}' is missing or invalid.");
            }

            return $"{baseUrl}{endpoint}";
        }


        public static string GetToken(IHttpContextAccessor _httpContextAssessor)
        {
            var token = _httpContextAssessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                throw new UserFriendlyException(ErrorCode.Unauthorized, "Token is missing.");
            }
            return token;
        }
    }
}
