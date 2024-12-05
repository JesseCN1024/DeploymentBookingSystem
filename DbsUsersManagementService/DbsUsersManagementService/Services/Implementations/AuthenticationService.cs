using DbsUsersManagementService.Data;
using DbsUsersManagementService.Models.DTOs;
using DbsUsersManagementService.Services.Interfaces;
using DbsUsersManagementService.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace DbsUsersManagementService.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private IValidator<RegisterRequestDto> registerValidator;
        private UserManager<IdentityUser> userManager;
        private DbsUserAuthDbContext context;

        public AuthenticationService(UserManager<IdentityUser> userManager, IValidator<RegisterRequestDto> registerValidator, DbsUserAuthDbContext context)
        {
            this.registerValidator = registerValidator;
            this.userManager = userManager;
            this.context = context;
        }
        public async Task<IdentityResult> RegisterUserAsync(RegisterRequestDto registerRequest)
        {
            //try
            //{

            //    var teamExists  = await context.Teams.AnyAsync(t => t.teamId == registerRequest.teamId);
            //    ValidationHelper.ValidateRequest(registerValidator, registerRequest);

            //    var user = new IdentityUser
            //    {
            //        UserName = registerRequest.username,
            //        Email = registerRequest.email,

            //    };
            //    var result = await userManager.CreateAsync(user, registerRequest.Password);
            //    if (!result.Succeeded)
            //    {
            //        return BadRequest(result.Errors);
            //    }
            //    foreach (var role in registerRequest.Roles)
            //    {
            //        if (!await userManager.IsInRoleAsync(user, role))
            //        {
            //            await userManager.AddToRoleAsync(user, role);
            //        }
            //    }
            //    return Ok();
            //}
            //catch (ValidationException ex)
            //{
            //    return BadRequest(new { Errors = ex.Message });
            //}
            return null;
        }
    }
}
