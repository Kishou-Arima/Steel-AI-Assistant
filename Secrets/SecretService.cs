using System.Text.Json;

namespace Steel.Services
{
    public static class SecretService
    {
        private static string? _cachedKey;

        public static string GetOpenAIApiKey()
        {
            if (_cachedKey != null) return _cachedKey;

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Secrets", "secrets.json");

            if (!File.Exists(path))
                throw new FileNotFoundException("secrets.json not found.");

            var json = File.ReadAllText(path);
            using var doc = JsonDocument.Parse(json);
            _cachedKey = doc.RootElement.GetProperty("ApiKey").GetString();

            return _cachedKey!;
        }
    }
}
