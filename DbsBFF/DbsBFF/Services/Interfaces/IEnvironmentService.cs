namespace DbsBFF.Services.Interfaces
{
    public interface IEnvironmentService
    {
        Task<bool> EnvExistsAsync(Guid envId);
    }
}
