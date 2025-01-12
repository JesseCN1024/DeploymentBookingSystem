using DbsEnvManagementService.Data;
using DbsEnvManagementService.Models.Domain;
using DbsEnvManagementService.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DbsEnvManagementService.Repositories
{
    public class EnvRepository : IEnvRepository
    {
        private AppDbContext _context;

        public EnvRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Env> CreateEnvAsync(Env env)
        {
            await _context.Environments.AddAsync(env);
            await _context.SaveChangesAsync();
            return env;
        }

        public async Task<List<Env>> GetAllEnvsAsync(SearchRequestDto requestDto)
        {
            var query = _context.Environments.AsQueryable();

            if (!string.IsNullOrEmpty(requestDto.searchKey))
            {
                query = query.Where(env => env.Name.Contains(requestDto.searchKey));
            }

            if (requestDto.Pagination.Page > 0 && requestDto.Pagination.PageSize > 0)
            {
                query = query.Skip((requestDto.Pagination.Page - 1) * requestDto.Pagination.PageSize).Take(requestDto.Pagination.PageSize);
            }

            foreach (var sort in requestDto.Sorts)
            {
                query = sort.Name switch
                {
                    "createdDate" => sort.Direction == "Ascending"
                        ? query.OrderBy(u => u.CreatedDate)
                        : query.OrderByDescending(u => u.CreatedDate),
                    "name" => sort.Direction == "Ascending"
                        ? query.OrderBy(u => u.Name)
                        : query.OrderByDescending(u => u.Name),
                    _ => query
                };
            }

            return await query.ToListAsync();
        }

        public async Task<Env?> GetEnvAsync(Guid id)
        {
            return await _context.Environments.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task UpdateEnvAsync(Env env)
        {
            _context.Environments.Update(env);
            await _context.SaveChangesAsync();
        }
    }
}
