using DbsEnvManagementService.Models.Domain;
using DbsEnvManagementService.Models.DTOs;

namespace DbsEnvManagementService.Services.Interfaces
{
    public interface IEnvService
    {

        Task<Guid> CreateEnvAsync(UpdateRequestDto request);

        Task UpdateEnvAsync(Guid envId, UpdateRequestDto request);

        Task DeleteEnvAsync(Guid envId);

        Task<EnvResponseDto?> GetEnvByIdAsync(Guid id);

        Task<SearchResponseDto> GetAllEnvsAsync(SearchRequestDto requestDto);
        

    }
}
