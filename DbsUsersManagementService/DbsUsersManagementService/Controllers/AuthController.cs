using DbsUsersManagementService.Models.DTOs;
using DbsUsersManagementService.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace DbsUsersManagementService.Controllers
{
    [Route("users")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<IdentityUser> userManager;
        private IValidator<RegisterRequestDto> registerValidator;

        public AuthController(UserManager<IdentityUser> userManager, IValidator<RegisterRequestDto> registerValidator )
        {
            this.userManager = userManager;
            this.registerValidator = registerValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequest)
        {
            return Ok();
            
        }
    }
}
