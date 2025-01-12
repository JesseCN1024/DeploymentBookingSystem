using DbsUsersManagementService.Models.Domain;
using DbsUsersManagementService.Models.DTOs;

namespace DbsUsersManagementService.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<User?> GetUserByNameAsync(string username);
        Task<List<User>> GetAllUsersAsync(UserSearchRequestDto requestDto);

        Task<User> CreateUserAsync(User user);
        Task UpdateUserAsync(User user);


        Task HardDeleteUserAsync(User user);


        Task<Team?> GetTeamById(Guid teamId);
        Task<Role?> GetRoleById(Guid roleId);

    }

}
