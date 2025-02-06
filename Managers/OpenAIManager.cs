using System.Net.Http.Headers;
using TestDataInator.Models;

namespace TestDataInator.Managers
{
    public interface IOpenAIManager
    {
        Task<string> Prompt(string prompt);
    }

    public class OpenAIManager : IOpenAIManager
    {
        private readonly ILogger<OpenAIManager> _logger;
        private HttpClient _client = new HttpClient();
        private string _baseUrl = "https://api.openai.com/v1/chat";
        private readonly IConfiguration _configuration;

        public OpenAIManager(ILogger<OpenAIManager> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<string> Prompt(string prompt)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/completions"))
            {
                requestMessage.Content = JsonContent.Create(new OpenAIRequest()
                {
                    model = "gpt-3.5-turbo",
                    messages = new List<OpenAIMessage>() {
                        new OpenAIMessage() {
                            role = "user",
                            content = prompt
                        }
                    },
                    temperature = 0.7
                });

                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["OpenAIKey"]);

                OpenAIResponse obj;

                HttpResponseMessage response = await _client.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    obj = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
                }
                else
                {
                    throw new Exception($"{response.StatusCode}: {response.ReasonPhrase}");
                }

                return obj?.choices?.FirstOrDefault()?.message?.content;
            }
        }
    }
}
