using TestDataInator.Managers;
using TestDataInator.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TestDataInator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IOllamaManager _ollamaManager;
        private readonly IOpenAIManager _openAIManager;

        public TestController(ILogger<TestController> logger, IOllamaManager ollamaManager, IOpenAIManager openAIManager)
        {
            _logger = logger;
            _ollamaManager = ollamaManager;
            _openAIManager = openAIManager;
        }

        [HttpGet("Ollama")]
        public async Task<Object> TryOllama(string request)
        {
            return new
            {
                response = await _ollamaManager.Prompt(request)
            };
        }


        [HttpGet("OpenAI")]
        public async Task<Object> TryOpenAI(string request)
        {
            return new
            {
                response = await _openAIManager.Prompt(request)
            };
        }
    }
}
