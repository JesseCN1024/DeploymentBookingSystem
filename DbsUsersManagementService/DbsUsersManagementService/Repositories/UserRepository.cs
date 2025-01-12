using DbsUsersManagementService.Data;
using DbsUsersManagementService.Models.Domain;
using DbsUsersManagementService.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DbsUsersManagementService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>(); // Simulated database for demo purposes
        private DbsUserAuthDbContext _context;


        public UserRepository(DbsUserAuthDbContext context)
        {
            _context = context;
            
        }


        // User methods
        // GET
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users
                .Include("Team")
                .Include("Role")
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }
        public async Task<User?> GetUserByNameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username && !u.IsDeleted);
        }


        public async Task<List<User>> GetAllUsersAsync(UserSearchRequestDto requestDto)
        {
            var query = _context.Users.AsQueryable();

            // Apply search filters
            if (requestDto.SearchFields.UserIds?.Any() == true)
                query = query.Where(u => requestDto.SearchFields.UserIds.Contains(u.Id));

            if (requestDto.SearchFields.TeamIds?.Any() == true)
                query = query.Where(u => requestDto.SearchFields.TeamIds.Contains(u.TeamId));

            if (requestDto.SearchFields.RoleIds?.Any() == true)
                query = query.Where(u => requestDto.SearchFields.RoleIds.Contains(u.RoleId));

            if (requestDto.SearchFields.UserNames?.Any() == true)
                query = query.Where(u => requestDto.SearchFields.UserNames.Contains(u.UserName));

            // Apply sorting
            foreach (var sort in requestDto.Sorts)
            {
                query = sort.Name switch
                {
                    "createdDate" => sort.Direction == "Ascending"
                        ? query.OrderBy(u => u.CreatedDate)
                        : query.OrderByDescending(u => u.CreatedDate),
                    "username" => sort.Direction == "Ascending"
                        ? query.OrderBy(u => u.UserName)
                        : query.OrderByDescending(u => u.UserName),
                    _ => query
                };
            }

            return await query.ToListAsync();
        }

        // Create
        public async Task<User> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        

      
        // Update

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();          
        }


        // Delete

        


        public async Task HardDeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }


        // Team
        public async Task<Team?> GetTeamById(Guid teamId)
        {
            return await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
        }



        // Role 
        public async Task<Role?> GetRoleById(Guid roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(t => t.Id == roleId);
        }

        

        
    }
}
