using DbsUsersManagementService.Models.DTOs;

namespace DbsUsersManagementService.Services.Interfaces
{
    public interface IAuthenService
    {
        //Task<Team?> GetTeamById(Guid teamId, bool isThrowException = true);
        //Task<Role?> GetRoleById(Guid roleId, bool isThrowException = true);
        //Task<Role?> GetUserById(Guid userId, bool isThrowException = true);


        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto loginRequest);
        Task<Guid> RegisterUserAsync(RegisterRequestDto registerRequest);
        Task<UpdateResponseDto?> UpdateUserAsync(UpdateRequestDto updateRequest, Guid userId);
        Task<UserSearchResponseDto> GetAllUsersAsync(UserSearchRequestDto requestDto);
        Task SoftDeleteUserAsync(Guid userId);
        Task HardDeleteUserAsync(Guid userId);

        
    }
}
