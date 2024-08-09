using AdSystem.Models;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AdSystem.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SubscriberDto> GetSubscriberAsync(int subscriptionNumber)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5001/api/subscriber/{subscriptionNumber}");
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubscriberDto>();
            }
            return null;
        }

        public async Task<SubscriberDto> AddSubscriberAsync(SubscriberDto subscriber)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5001/api/subscriber", subscriber);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubscriberDto>();
            }
            return null;
        }
    }
}
