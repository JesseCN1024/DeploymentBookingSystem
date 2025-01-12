namespace DbsUsersManagementService.Models.DTOs
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; } = 3600; // in seconds
        public string TokenType { get; set; } = "Bearer";
    }
}
