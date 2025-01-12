namespace DbsUsersManagementService.Models.DTOs
{
    public class UpdateRequestDto 
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public Guid TeamId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
