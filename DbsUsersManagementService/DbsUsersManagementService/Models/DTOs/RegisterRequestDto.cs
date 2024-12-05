namespace DbsUsersManagementService.Models.DTOs
{
    public class RegisterRequestDto
    {
        public required string username { get; set; }
        public required string email { get; set; }
        public required Guid teamId { get; set; }
        public required Guid roleId { get; set; }




    }
}
