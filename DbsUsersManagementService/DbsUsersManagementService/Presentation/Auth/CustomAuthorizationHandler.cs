﻿using DbsUsersManagementService.Models.Domain;
using DbsUsersManagementService.Presentation.Constants;
using DbsUsersManagementService.Utilities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DbsUsersManagementService.Presentation.Auth
{
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomAuthorizationRequirement requirement)
        {
            foreach (var claim in context.User.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
            // Check if the user has one of the allowed roles
            var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == null || !requirement.AllowedRoles.Contains(userRole))
            {
                throw new UserFriendlyException(ErrorCode.Unauthorized, $"User is unauthorized to perform the action.");
                //return Task.CompletedTask; // Role condition failed
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
