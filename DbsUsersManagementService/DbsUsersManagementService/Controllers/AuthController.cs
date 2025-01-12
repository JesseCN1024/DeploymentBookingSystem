using AutoMapper;
using DbsUsersManagementService.Models.DTOs;
using DbsUsersManagementService.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace DbsUsersManagementService.Controllers
{
    [ApiController]
    [Route("users")]
    public class AuthController : ControllerBase
    {
        private IAuthenService _authenService;
        private readonly IMapper _mapper;
        private ILogger<AuthController> _logger;


        public AuthController(IValidator<RegisterRequestDto> registerValidator, IAuthenService authenService, ILogger<AuthController> logger, IMapper mapper)
        {
            _authenService = authenService;
            _logger = logger;
            _mapper = mapper;
        }


        [Authorize(Policy = "AllUsers")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid userId)
        {
            
            var user = await _authenService.GetUserByIdAsync(userId);
            return Ok(user);
            
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("search")]
        public async Task<IActionResult> GetAll([FromBody] UserSearchRequestDto userSearchRequestDto)
        {
            var users = await _authenService.GetAllUsersAsync(userSearchRequestDto);
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var response = await _authenService.AuthenticateAsync(loginRequest);
            return Ok(response);    
        }



        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            var userId = await _authenService.RegisterUserAsync(registerRequest);

            if (userId != Guid.Empty)
            {
                var locationUrl = Url.Action(nameof(GetById), new { userId });
                return Created(locationUrl, new { Message = "User registered successfully" });
            }

            else
            {
                return BadRequest();
            }
        }


        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{userId:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid userId, 
            [FromBody] UpdateRequestDto updateRequest)
        {
            var useDto = await _authenService.UpdateUserAsync(updateRequest, userId);
            return Ok(useDto);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{userId:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid userId)
        {

            try
            {
                await _authenService.SoftDeleteUserAsync(userId);
                return Ok(new { Message = "User deleted successfully!" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }


        }

        public static class Role
        {
            public const string Admin = "ADMIN2";
            public const string PowerUser = "ADMIN";
            public const string GeneralUser = "ADMIN";

        }// constants 
    }
}
