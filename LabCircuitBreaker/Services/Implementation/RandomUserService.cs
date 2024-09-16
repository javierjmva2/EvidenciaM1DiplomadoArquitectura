using LabCircuitBreaker.Models;
using LabCircuitBreaker.Services.Interface;
using System.Net.Http;

namespace LabCircuitBreaker.Services.Implementation
{
    public class RandomUserService : IRandomUserService
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public RandomUserService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("ResilientClientRandomUser");
            _configuration = configuration;

        }
        public async Task<ResultRandomUserModel> GetRandomUserAsync()
        {
            using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_configuration.GetSection("InternalAPI:Url").Value}/api/RandomUser/GetRandom"))
            {
                var response = await _httpClient.SendAsync(httpRequestMessage);
                response.EnsureSuccessStatusCode();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ResultRandomUserModel>(await response.Content.ReadAsStringAsync());
            }

        }
    }
}
