using TestDataInator.Helpers;
using TestDataInator.Managers;
using TestDataInator.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DataObjectController.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataObjectController : ControllerBase
    {
        private readonly ILogger<DataObjectController> _logger;
        private readonly IOllamaManager _ollamaManager;
        private readonly IOpenAIManager _openAIManager;
        private bool useOpenAI = false;

        public DataObjectController(ILogger<DataObjectController> logger, IOllamaManager ollamaManager, IOpenAIManager openAIManager)
        {
            _logger = logger;
            _ollamaManager = ollamaManager;
            _openAIManager = openAIManager;
        }

        [HttpPost("TestString")]
        public async Task<string> GetTestString([FromBody] Object format)
        {
            string prompt = @$"
            Please return a return a valid list of JSON objects which are in this format: {format.ToString()}. Surround the data with ``` .
            ";

            var response = useOpenAI ?
                await _openAIManager.Prompt(prompt) :
                await _ollamaManager.Prompt(prompt);

            return StringHelper.RemoveIllegalCharacters(response);
        }

        [HttpPost("TestObjects")]
        public async Task<object> GetTestObjects([FromBody] Object format)
        {
            string prompt = @$"
            Please return a return a valid list of JSON objects which are in this format: {format.ToString()}. Surround the data with ``` .
            ";
            object? answer = null;
            bool ok = false;
            int attempts = 0;

            while (!ok && attempts < 10)
            {
                var response = useOpenAI ?
                await _openAIManager.Prompt(prompt) :
                await _ollamaManager.Prompt(prompt);

                string? bob = StringHelper.RemoveIllegalCharacters(response);

                try
                {
                    answer = JsonConvert.DeserializeObject(bob);
                }
                catch (Exception e)
                {
                    attempts++;
                    _logger.LogError(e, "Error parsing JSON");
                }
            }

            return answer ?? new object();
        }
    }
}
