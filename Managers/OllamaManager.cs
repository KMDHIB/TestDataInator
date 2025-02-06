using TestDataInator.Models;

namespace TestDataInator.Managers
{
    public interface IOllamaManager
    {
        Task<string> Prompt(string prompt);
    }

    public class OllamaManager : IOllamaManager
    {
        private readonly ILogger<OllamaManager> _logger;
        private readonly IConfiguration _configuration;
        private HttpClient _client = new HttpClient();
        private string _baseUrl = "http://localhost:11434/api";

        public OllamaManager(ILogger<OllamaManager> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<string> Prompt(string prompt)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/generate"))
            {
                requestMessage.Content = JsonContent.Create(new OllamaRequest()
                {
                    model = _configuration["OllamaModel"],
                    prompt = prompt
                });

                OllamaResponse obj;

                HttpResponseMessage response = await _client.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    obj = await response.Content.ReadFromJsonAsync<OllamaResponse>();
                }
                else
                {
                    throw new Exception($"{response.StatusCode}: {response.ReasonPhrase}");
                }

                return obj.response;
            }
        }
    }
}
