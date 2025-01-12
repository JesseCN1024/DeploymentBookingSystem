using DbsUsersManagementService.Models.Base;

namespace DbsUsersManagementService.Models.Domain
{
    public class Team : AuditableEntity
    {
        public required string Name { get; set; }

    }
}
