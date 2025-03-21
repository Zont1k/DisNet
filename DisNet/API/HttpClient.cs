using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DiscordSharpLib.API
{
    public class DiscordHttpClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://discord.com/api/v10";

        public DiscordHttpClient(string token)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bot {token}");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "DiscordSharpLib (https://example.com, 1.0)");
        }

        public async Task SendMessageAsync(string channelId, string content)
        {
            var payload = new { content = content };
            var json = JsonConvert.SerializeObject(payload);
            var response = await _httpClient.PostAsync(
                $"{BaseUrl}/channels/{channelId}/messages",
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
        }
    }
}