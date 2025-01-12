using DbsUsersManagementService.Models.Base;

namespace DbsUsersManagementService.Models.Domain
{
    public class UserRole : AuditableEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

    }
}
