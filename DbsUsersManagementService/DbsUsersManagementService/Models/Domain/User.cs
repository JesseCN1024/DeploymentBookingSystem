using DbsUsersManagementService.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace DbsUsersManagementService.Models.Domain
{
    public class User : AuditableEntity
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }

        public Guid TeamId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsActive { get; set; }


        // Navigation Properties 
        public Role Role { get; set; }
        public Team Team { get; set; }

    }
}
