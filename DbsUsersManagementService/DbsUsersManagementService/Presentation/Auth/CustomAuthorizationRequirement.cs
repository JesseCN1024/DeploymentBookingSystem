using Microsoft.AspNetCore.Authorization;

namespace DbsUsersManagementService.Presentation.Auth
{
    public class CustomAuthorizationRequirement : IAuthorizationRequirement
    {
        public string[] AllowedRoles { get; }
        public Guid? RequiredTeamId { get; }

        public CustomAuthorizationRequirement(string[] allowedRoles, Guid? requiredTeamId = null)
        {
            AllowedRoles = allowedRoles;
            RequiredTeamId = requiredTeamId;
        }
    }
}
