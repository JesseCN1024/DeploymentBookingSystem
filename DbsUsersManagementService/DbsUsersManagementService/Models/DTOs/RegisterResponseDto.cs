namespace DbsUsersManagementService.Models.DTOs
{
    public class RegisterResponseDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string TeamId { get; set; }
        public string RoleId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
