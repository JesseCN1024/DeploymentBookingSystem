using AutoMapper;
using DbsUsersManagementService.Controllers;
using DbsUsersManagementService.Models.Domain;
using DbsUsersManagementService.Models.DTOs;
using DbsUsersManagementService.Presentation.Constants;
using DbsUsersManagementService.Repositories;
using DbsUsersManagementService.Services.Interfaces;
using DbsUsersManagementService.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DbsUsersManagementService.Services.Implementations
{
    public class AuthenService : IAuthenService
    {
        private IValidator<RegisterRequestDto> _registerValidator;
        private IUserRepository _repository;
        private ILogger<AuthController> _logger;
        private IMapper _mapper;
        private IConfiguration _configuration;
        private IPasswordHasher<string> _passwordHasher;
        private IHttpContextAccessor _httpContextAccessor;

        public AuthenService(IValidator<RegisterRequestDto> registerValidator, IUserRepository userRepository, ILogger<AuthController> logger, IMapper mapper, IConfiguration configuration, IPasswordHasher<string> passwordHasher, IHttpContextAccessor httpContextAccessor)
        {
            _registerValidator = registerValidator;
            _repository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<Team?> GetTeamById(Guid teamId, bool isThrowException = true)
        {
            var team = await _repository.GetTeamById(teamId);
            if (team == null && isThrowException)
            {
                throw new UserFriendlyException(ErrorCode.NotFound, $"TeamId '{teamId}' is not found.");
            }
            return team;
        }

        private async Task<Models.Domain.Role?> GetRoleById(Guid roleId, bool isThrowException = true)
        {
            var role = await _repository.GetRoleById(roleId);
            if (role == null && isThrowException)
            {
                throw new UserFriendlyException(ErrorCode.NotFound, $"RoleId '{roleId}' is not found.");
            }
            return role;
        }

        private async Task<bool> GetUserByNameAsync(String username, bool isThrowException = true)
        {
            var user = await _repository.GetUserByNameAsync(username.ToString());
            if (user != null && isThrowException)
            {
                throw new UserFriendlyException(ErrorCode.Conflict, $"UserName '{username}' already exists.");
            }
            return user != null;
        }

        private async Task<User?> GetExistUserByIdAsync(Guid userId, bool isThrowException = true)
        {
            var user = await _repository.GetUserByIdAsync(userId);
            if (user == null && isThrowException)
            {
                throw new UserFriendlyException(ErrorCode.NotFound, $"UserId '{userId}' is not found.");
            }
            return user;
        }


        private Guid GetUserIdFromClaims()
        {
            // Get the authenticated user's ID from the claims
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UserFriendlyException(ErrorCode.Unauthorized, "User is not authenticated.");
            }
            return Guid.Parse(userIdClaim.Value);
        }


        public async Task<UserSearchResponseDto> GetAllUsersAsync(UserSearchRequestDto requestDto)
        {


            // Fetch users from repository
            var users = await _repository.GetAllUsersAsync(requestDto);

            // Count total items
            var totalItems = users.Count;

            // Apply pagination
            users = users.Skip((requestDto.Pagination.Page - 1) * requestDto.Pagination.PageSize)
                         .Take(requestDto.Pagination.PageSize)
                         .ToList();

            // Map to response DTO
            return new UserSearchResponseDto
            {
                Data = users.Select(user => _mapper.Map<UserDto>(user)).ToList(),
                Pagination = new PaginationInfoDto
                {
                    Page = requestDto.Pagination.Page,
                    PageSize = requestDto.Pagination.PageSize,
                    TotalItems = totalItems
                }
            };
        }



        public async Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto loginRequest)
        {
            // Fetch user by username
            var user = await _repository.GetUserByNameAsync(loginRequest.UserName);

            if (user == null || !_passwordHasher.VerifyHashedPassword(user.UserName, user.Password, loginRequest.Password).Equals(PasswordVerificationResult.Success))
            {
                throw new UserFriendlyException(ErrorCode.Unauthorized, $"Invalid UserName or Password.");
            }

            // Ensure user is active
            if (!user.IsActive)
            {
                throw new UserFriendlyException(ErrorCode.Forbidden, $"User is not active now .");
            }

            // Get role of user
            var role = await GetRoleById(user.RoleId);

            // Generate JWT
            var token = GenerateJwt(user, role.Name);

            return new LoginResponseDto
            {
                AccessToken = token,
                ExpiresIn = 3600,
                TokenType = "Bearer"
            };
        }

     
        private string GenerateJwt(User user, String role="GENERAL_USER")
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("TeamId", user.TeamId.ToString()),
                new Claim("TeamName", user.TeamId != Guid.Empty ? "SampleTeam" : "Unknown"), // Replace with actual team name fetching logic
                new Claim(ClaimTypes.Role, role)
            };
    
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(3600),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<Guid> RegisterUserAsync(RegisterRequestDto registerRequest)
        {
            // no need to validate 
            // Check teamId and roleId exist in database and gain to variables
            var team = await GetTeamById(registerRequest.TeamId);

            var role = await GetRoleById(registerRequest.RoleId);

            var userCheck = await GetUserByNameAsync(registerRequest.Username);


            // Starting to create user 
            var user = new User
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Email,
                Password = _passwordHasher.HashPassword(registerRequest.Username, "Enter@9191"),
                TeamId = registerRequest.TeamId,
                RoleId = registerRequest.RoleId,
                IsActive = true,
                //CreatedBy = GetUserIdFromClaims(), // Guid ID
                CreatedBy = Guid.Parse("b5d8a40e-bb23-4059-aba0-9a4811924f34"), // Guid ID
                CreatedDate = DateTime.UtcNow, // Guid ID
                //UpdatedBy = GetUserIdFromClaims(),
                UpdatedBy = Guid.Parse("b5d8a40e-bb23-4059-aba0-9a4811924f34"),
                UpdatedDate = DateTime.UtcNow
            };


            try
            {
                var result = await _repository.CreateUserAsync(user);
                return (result.Id);

            }

            catch(Exception e)
            {
                throw new UserFriendlyException(ErrorCode.ServiceUnavailable, $"Failed to create new user.");
                //return result;
            }


        }


        public async Task<UpdateResponseDto?> UpdateUserAsync(UpdateRequestDto updateRequest, Guid id)
        {
            // Check teamId and roleId exist in database and gain to variables
            var team = await GetTeamById(updateRequest.TeamId);
            var role = await GetRoleById(updateRequest.RoleId);

            var existingUser = await GetExistUserByIdAsync(id);

            if (existingUser != null)
            {
                existingUser.UserName = updateRequest.Username;
                existingUser.Email = updateRequest.Email;
                existingUser.TeamId = updateRequest.TeamId;
                existingUser.RoleId = updateRequest.RoleId;
                existingUser.IsActive = updateRequest.IsActive;
                existingUser.UpdatedBy = GetUserIdFromClaims();
                existingUser.UpdatedDate = DateTime.UtcNow;
                await _repository.UpdateUserAsync(existingUser);
                return _mapper.Map<UpdateResponseDto>(existingUser);

            }
            return null;


        }


        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var userModel = await GetExistUserByIdAsync(id);
            
            return _mapper.Map<UserDto>(userModel);


        }


        public async Task SoftDeleteUserAsync(Guid userId)
        {
            var existingUser = await GetExistUserByIdAsync(userId);

            if (existingUser != null)
            {
                existingUser.IsDeleted = true;
                // parse
                existingUser.UpdatedBy = GetUserIdFromClaims();
                existingUser.UpdatedDate = DateTime.UtcNow; 

                await _repository.UpdateUserAsync(existingUser);

            }
        }


        public async Task HardDeleteUserAsync(Guid userId)
        {
            var existingUser = await GetExistUserByIdAsync(userId);   
            if (existingUser!=null)
            {
                await _repository.HardDeleteUserAsync(existingUser);
            }
        }

        
    }
}
