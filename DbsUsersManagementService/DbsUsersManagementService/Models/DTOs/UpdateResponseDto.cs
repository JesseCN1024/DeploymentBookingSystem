using DbsUsersManagementService.Models.Base;

namespace DbsUsersManagementService.Models.DTOs
{
    public class UpdateResponseDto : AuditableEntity
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public bool IsActive { get; set; }
        public Guid TeamId { get; set; }
        public Guid RoleId { get; set; }

    }
}
