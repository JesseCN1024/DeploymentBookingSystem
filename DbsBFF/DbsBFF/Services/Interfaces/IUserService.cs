using DbsBFF.Models.DTOs;

namespace DbsBFF.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto requesetDto);
        Task<bool> UserExistsAsync(Guid userId);


    }
}
