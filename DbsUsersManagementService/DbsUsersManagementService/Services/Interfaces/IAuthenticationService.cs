using DbsUsersManagementService.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace DbsUsersManagementService.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterRequestDto registerRequest);
    }
}
