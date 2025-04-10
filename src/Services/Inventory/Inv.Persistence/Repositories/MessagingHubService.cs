using Inv.Application.Interfaces.Repositories;
using Inv.Application.Request;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Inv.Persistence.Repositories
{
    public class MessagingHubService : IMessagingHubService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public MessagingHubService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["MessagingHub:BaseUrl"]; // Fetch from appsettings.json
        }

        public async Task<bool> SendMessageAsync(CreateUserMessageRequest messageRequest)
        {
            var url = $"{_baseUrl}/send"; // Assuming `/send` is the endpoint
            var jsonContent = new StringContent(JsonSerializer.Serialize(messageRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsonContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CreateUserMessageRequest>> GetMessagesAsync(int userId)
        {
            var url = $"{_baseUrl}/receive?userId={userId}"; // Assuming `/receive` is the endpoint
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<CreateUserMessageRequest>>(content) ?? new List<CreateUserMessageRequest>();
            }

            throw new Exception("Failed to fetch messages from external service");
        }
    }
}
