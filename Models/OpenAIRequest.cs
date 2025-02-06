namespace TestDataInator.Models
{
    public class OpenAIRequest
    {
        public string model { get; set; }
        public List<OpenAIMessage> messages { get; set; }
        public double temperature { get; set; } = 0.7;
    }

    public class OpenAIMessage
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}
