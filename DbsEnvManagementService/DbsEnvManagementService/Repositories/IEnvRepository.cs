using DbsEnvManagementService.Data;
using DbsEnvManagementService.Models.Domain;
using DbsEnvManagementService.Models.DTOs;

namespace DbsEnvManagementService.Repositories
{
    public interface IEnvRepository
    {
        Task<Env> CreateEnvAsync(Env env);
        Task UpdateEnvAsync(Env env);

        Task<Env?> GetEnvAsync(Guid id);

        Task<List<Env>> GetAllEnvsAsync(SearchRequestDto requestDto);




    }
}
