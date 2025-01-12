using DbsUsersManagementService.Models.Base;
using DbsUsersManagementService.Models.Domain;

namespace DbsUsersManagementService.Models.DTOs
{
    public class UserDto : AuditableEntity
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public bool IsActive { get; set; }


        // Navigation Properties 
        public Role Role { get; set; }
        public Team Team { get; set; }
    }
}
