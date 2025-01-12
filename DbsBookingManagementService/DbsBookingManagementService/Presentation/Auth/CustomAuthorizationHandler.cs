using DbsBookingManagementService.Presentation.Constants;
using DbsBookingManagementService.Utilities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DbsBookingManagementService.Presentation.Auth
{
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomAuthorizationRequirement requirement)
        {
            // Check if the user has one of the allowed roles
            var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == null || !requirement.AllowedRoles.Contains(userRole))
            {
                throw new UserFriendlyException(ErrorCode.Unauthorized, $"User is unauthorized to perform the action.");
            }

            // Check if the user's team matches the required team (if specified)
            if (requirement.RequiredTeamId.HasValue)
            {
                var userTeamId = context.User.FindFirst("TeamId")?.Value;
                if (userTeamId == null || userTeamId != requirement.RequiredTeamId.Value.ToString())
                {
                    return Task.CompletedTask; // Team condition failed
                }
            }

            // All conditions met
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

}
