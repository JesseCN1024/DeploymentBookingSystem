using DbsUsersManagementService.Models.Base;

namespace DbsUsersManagementService.Models.Domain
{
    public class Role : AuditableEntity
    {
        public required string Name { get; set; }
        public required string DisplayName { get; set; }

    }
}
