using LabCircuitBreaker.Models;

namespace LabCircuitBreaker.Api.Services.Interface
{
    public interface IRandomUserService
    {
        Task<ResultRandomUserModel> GetRandomUserAsync();
    }
}
