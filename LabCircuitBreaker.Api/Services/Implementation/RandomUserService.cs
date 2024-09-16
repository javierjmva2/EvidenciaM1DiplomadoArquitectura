using LabCircuitBreaker.Api.Services.Interface;
using LabCircuitBreaker.Models;

namespace LabCircuitBreaker.Api.Services.Implementation
{
    public class RandomUserService : IRandomUserService
    {
        private static HttpClient _httpClient;

        public RandomUserService()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
        }

        public async Task<ResultRandomUserModel> GetRandomUserAsync()
        {
            using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://randomuser.me/api/"))
            {
                var response = await _httpClient.SendAsync(httpRequestMessage);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("API Random user error.");

                return Newtonsoft.Json.JsonConvert.DeserializeObject<ResultRandomUserModel>(await response.Content.ReadAsStringAsync());
            }

        }
    }
}
