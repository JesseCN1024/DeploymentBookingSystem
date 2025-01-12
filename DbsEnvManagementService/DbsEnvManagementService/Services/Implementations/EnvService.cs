using AutoMapper;
using DbsEnvManagementService.Models.Domain;
using DbsEnvManagementService.Models.DTOs;
using DbsEnvManagementService.Presentation.Constants;
using DbsEnvManagementService.Repositories;
using DbsEnvManagementService.Services.Interfaces;
using DbsEnvManagementService.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DbsEnvManagementService.Services.Implementations
{
    public class EnvService : IEnvService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IEnvRepository _repository;
        private IConfiguration _configuration;
        private IMapper _mapper;

        public EnvService(IHttpContextAccessor httpContextAccessor, IEnvRepository repository, IConfiguration configuration, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }



        private ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, 
                    out SecurityToken validatedToken);
                return principal;
            }
            catch
            {
                throw new UserFriendlyException(ErrorCode.Unauthorized, "Invalid token.");
            }
        }

        private Guid GetUserIdFromClaims()
        {
            var token = _httpContextAccessor.HttpContext?.Request
                .Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                throw new UserFriendlyException(ErrorCode.Unauthorized, "Token is missing.");
            }

            var principal = ValidateToken(token);
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UserFriendlyException(
                    ErrorCode.Unauthorized, "User is not authenticated.");
            }

            return Guid.Parse(userIdClaim.Value);
        }


        public async Task<Guid> CreateEnvAsync(UpdateRequestDto request)
        {
            var userId = GetUserIdFromClaims();
            var env = new Env
            {
                Name = request.Name,
                DisplayName = request.DisplayName,
                Account = request.Account,
                Stack = request.Stack,
                Notes = request.Notes,
                Status = request.Status,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = userId,
                UpdatedDate = DateTime.UtcNow
            };

            try
            {
                var result = await _repository.CreateEnvAsync(env);
                return result.Id;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ErrorCode.ServiceUnavailable, ex.Message);
            }
        }

        public async Task UpdateEnvAsync(Guid envId, UpdateRequestDto request)
        {
            var existingEnv = await GetExistEnvByIdAsync(envId);

            if (existingEnv != null)
            {
                existingEnv.Name = request.Name;
                existingEnv.DisplayName = request.DisplayName;
                existingEnv.Account = request.Account;
                existingEnv.Stack = request.Stack;
                existingEnv.Notes = request.Notes;
                existingEnv.Status = request.Status;
                existingEnv.UpdatedBy = GetUserIdFromClaims();
                existingEnv.UpdatedDate = DateTime.UtcNow;
                try
                {
                    await _repository.UpdateEnvAsync(existingEnv);
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException(ErrorCode.ServiceUnavailable, $"Failed to create new env.");
                }
            }
        }


        public async Task DeleteEnvAsync(Guid envId)
        {
            var existingEnv = await GetExistEnvByIdAsync(envId);

            if (existingEnv != null)
            {
                if (existingEnv.Status == "Booked")
                {
                    throw new UserFriendlyException(ErrorCode.BadRequest, $"Environment '{envId}' is currently booked.");
                }

                existingEnv.IsDeleted = true;
                // parse
                existingEnv.UpdatedBy = GetUserIdFromClaims();
                existingEnv.UpdatedDate = DateTime.UtcNow;

                await _repository.UpdateEnvAsync(existingEnv);

            }
        }

        // Supporting methods 
        private async Task<Env?> GetExistEnvByIdAsync(Guid envId, bool isThrowException = true)
        {
            var user = await _repository.GetEnvAsync(envId);
            if (user == null && isThrowException)
            {
                throw new UserFriendlyException(ErrorCode.NotFound, $"Environment '{envId}' is not found.");
            }
            return user;
        }

        public async Task<EnvResponseDto?> GetEnvByIdAsync(Guid id)
        {
            var envModel = await GetExistEnvByIdAsync(id);
            return _mapper.Map<EnvResponseDto>(envModel);
        }

        public async Task<SearchResponseDto> GetAllEnvsAsync(SearchRequestDto requestDto)
        {
            var envs = await _repository.GetAllEnvsAsync(requestDto);
            var count = envs.Count;

            return new SearchResponseDto
            {
                Data = envs.Select(env => _mapper.Map<EnvResponseDto>(env)).ToList(),
                Pagination = new PaginationInfoDto
                {
                    Page = requestDto.Pagination.Page,
                    PageSize = requestDto.Pagination.PageSize,
                    Count = count
                }

            };
        }
    }
}
