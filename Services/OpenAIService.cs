using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Steel.Services
{
    public class OpenAIService
    {
        private readonly HttpClient _client = new();

        public async Task<string?> GetResponseAsync(string userInput, string apiKey, string model = "gpt-3.5-turbo")
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model,
                messages = new[]
                {
                        new { role = "system", content = "You are Steel, a helpful assistant." },
                        new { role = "user", content = userInput }
                    }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseJson);
            var choices = doc.RootElement.GetProperty("choices");
            if (choices.ValueKind == JsonValueKind.Array && choices.GetArrayLength() > 0)
            {
                var message = choices[0].GetProperty("message");
                if (message.TryGetProperty("content", out var contentProperty))
                {
                    return contentProperty.GetString();
                }
            }

            return null;
        }
    }
}
