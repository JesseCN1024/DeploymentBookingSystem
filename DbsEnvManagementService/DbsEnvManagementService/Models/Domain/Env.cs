using DbsEnvManagementService.Models.Base;
using System.Security.AccessControl;

namespace DbsEnvManagementService.Models.Domain
{
    public class Env : AuditableEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Account { get; set; }
        public string Stack { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; } // Available, Booked, Broken
        

    }
}
