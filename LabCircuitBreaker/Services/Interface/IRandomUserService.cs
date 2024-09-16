using LabCircuitBreaker.Models;

namespace LabCircuitBreaker.Services.Interface
{
    public interface IRandomUserService
    {
        Task<ResultRandomUserModel> GetRandomUserAsync();
    }
}
